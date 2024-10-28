using AutoMapper;
using FIAP.GestaoEscolar.Domain.Base;
using FIAP.GestaoEscolar.Domain.Commands.Class;
using FIAP.GestaoEscolar.Domain.Queries.Class;
using FIAP.GestaoEscolar.Infrastructure.Entities;
using FIAP.GestaoEscolar.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace FIAP.GestaoEscolar.Application.Services.Implementations
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        const string _cacheKeyList = "classList";
        const string _cacheKeyId = "class-{0}";

        public ClassService(IClassRepository classRepository, IMemoryCache cache, IMapper mapper)
        {
            _classRepository = classRepository;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<BaseResponse<int>> CreateAsync(CreateClassRequest request)
        {
            try
            {
                var classEntityMapper = _mapper.Map<Class>(request);
                int id = await _classRepository.CreateAsync(classEntityMapper);

                if (id == 0)
                    return new BaseResponse<int>(false, "Não foi possível cadastrar turma.", 0);

                _cache.Remove(_cacheKeyList);

                return new BaseResponse<int>(true, "Turma cadastrada com sucesso.", id);
            }
            catch (Exception ex)
            {
                return new BaseResponse<int>(false, ex.Message, 0);
            }
        }

        public async Task<BaseResponse> UpdateAsync(UpdateClassRequest request)
        {
            try
            { 
                var classEntity = await _classRepository.GetByIdAsync(request.Id);
                if (classEntity == null)
                    return new BaseResponse(false, "Turma não encontrada.");

                var classEntityMapper = _mapper.Map(request, classEntity);

                bool updated = await _classRepository.UpdateAsync(classEntityMapper);

                if (!updated)
                    return new BaseResponse(false, "Não foi possível atualizar turma.");

                _cache.Remove(_cacheKeyList);
                _cache.Remove(string.Format(_cacheKeyId, request.Id));

                return new BaseResponse(true, "Turma atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message);
            }
        }

        public async Task<BaseResponse> UpdateActiveAsync(int id, bool active)
        {
            try
            {
                string menssageSuccess = $"Turma {(!active ? "inativada" : "ativada")} com sucesso";

                var classResponse = (await GetByIdAsync(id)).Data;
                if (classResponse == null)
                    return new BaseResponse(false, "Turma não encontrada.");

                if (classResponse.Active == active)
                    return new BaseResponse(true, menssageSuccess);

                bool updated = await _classRepository.UpdateActiveAsync(id, active);

                if (!updated)
                    return new BaseResponse(false, $"Não foi possível atualizar turma.");

                _cache.Remove(_cacheKeyList);
                _cache.Remove(string.Format(_cacheKeyId, id));

                return new BaseResponse(true, menssageSuccess);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message);
            }
        }

        public async Task<BaseResponse<GetClassResponse?>> GetByIdAsync(int id)
        {
            try
            {
                string formatCacheKey = string.Format(_cacheKeyId, id);
                GetClassResponse? classResponse = null;

                if (!_cache.TryGetValue(formatCacheKey, out classResponse))
                {
                    var classEntity = await _classRepository.GetByIdAsync(id);

                    if (classEntity == null || classEntity?.Id == 0)
                        return new BaseResponse<GetClassResponse?>(false, "Nenhum dado encontrado.", null);

                    classResponse = _mapper.Map<GetClassResponse>(classEntity);

                    _cache.Set(formatCacheKey, classResponse, TimeSpan.FromMinutes(10));
                }

                return new BaseResponse<GetClassResponse?>(true, "Dado encontrado com sucesso.", classResponse);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetClassResponse?>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse<List<GetClassResponse>>> GetAllAsync()
        {
            try
            {
                List<GetClassResponse>? classesResponse = null;

                if (!_cache.TryGetValue(_cacheKeyList, out classesResponse))
                {
                    var classesEntity = await _classRepository.GetAllAsync();

                    if (classesEntity == null || classesEntity.Count == 0)
                        return new BaseResponse<List<GetClassResponse>>(false, "Nenhum dado encontrado.", null);

                    classesResponse = _mapper.Map<List<GetClassResponse>>(classesEntity);

                    _cache.Set(_cacheKeyList, classesResponse, TimeSpan.FromMinutes(10));
                }

                return new BaseResponse<List<GetClassResponse>>(true, "Dados encontrados com sucesso.", classesResponse);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<GetClassResponse>>(false, ex.Message, null);
            }
        }
        public async Task<bool> ClassNameExistsAsync(string className, int? id = 0)
        {
            return (await _classRepository.ClassNameExistsAsync(className, id)) > 0;
        }
    }
}
