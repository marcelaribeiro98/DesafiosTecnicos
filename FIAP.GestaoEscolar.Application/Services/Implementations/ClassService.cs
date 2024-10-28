using AutoMapper;
using FIAP.GestaoEscolar.Domain.Requests.Class;
using FIAP.GestaoEscolar.Domain.Responses.Base;
using FIAP.GestaoEscolar.Domain.Responses.Class;
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
                var entityMapper = _mapper.Map<Class>(request);
                int id = await _classRepository.CreateAsync(entityMapper);

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
                var entity = await _classRepository.GetByIdAsync(request.Id);
                if (entity == null)
                    return new BaseResponse(false, "Turma não encontrada.");

                var entityMapper = _mapper.Map(request, entity);

                bool updated = await _classRepository.UpdateAsync(entityMapper);

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

                var response = (await GetByIdAsync(id)).Data;
                if (response == null)
                    return new BaseResponse(false, "Turma não encontrada.");

                if (response.Active == active)
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
                GetClassResponse? response = null;

                if (!_cache.TryGetValue(formatCacheKey, out response))
                {
                    var entity = await _classRepository.GetByIdAsync(id);

                    if (entity == null || entity?.Id == 0)
                        return new BaseResponse<GetClassResponse?>(false, "Nenhum dado encontrado.", null);

                    response = _mapper.Map<GetClassResponse>(entity);

                    _cache.Set(formatCacheKey, response, TimeSpan.FromMinutes(10));
                }

                return new BaseResponse<GetClassResponse?>(true, "Dado encontrado com sucesso.", response);
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
                List<GetClassResponse>? response = null;

                if (!_cache.TryGetValue(_cacheKeyList, out response))
                {
                    var entities = await _classRepository.GetAllAsync();

                    if (entities == null || entities.Count == 0)
                        return new BaseResponse<List<GetClassResponse>>(false, "Nenhum dado encontrado.", null);

                    response = _mapper.Map<List<GetClassResponse>>(entities);

                    _cache.Set(_cacheKeyList, response, TimeSpan.FromMinutes(10));
                }

                return new BaseResponse<List<GetClassResponse>>(true, "Dados encontrados com sucesso.", response);
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
