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

        public async Task<BaseResponse<int?>> CreateAsync(CreateClassRequest request)
        {
            try
            {
                var entityMapper = _mapper.Map<ClassEntity>(request);
                int? id = await _classRepository.CreateAsync(entityMapper);

                if (id == null || id == 0)
                    return new BaseResponse<int?>(false, "Não foi possível cadastrar turma.");

                InvalidateCache();

                return new BaseResponse<int?>(true, "Turma cadastrada com sucesso.", id);
            }
            catch (Exception ex)
            {
                return new BaseResponse<int?>(false, ex.Message);
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
                bool? updated = await _classRepository.UpdateAsync(entityMapper);

                if (updated == null || !(bool)updated)
                    return new BaseResponse(false, "Não foi possível atualizar turma.");

                InvalidateCache(request.Id);

                return new BaseResponse(true, "Turma atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message);
            }
        }

        public async Task<BaseResponse> UpdateActiveAsync(int id)
        {
            try
            {
                var response = (await GetByIdAsync(id)).Data;
                if (response == null)
                    return new BaseResponse(false, "Turma não encontrada.");

                bool? updated = await _classRepository.UpdateActiveAsync(id, !response.Active);
                if (updated == null || (bool)!updated)
                    return new BaseResponse(false, $"Não foi possível atualizar turma.");

                InvalidateCache(id);

                return new BaseResponse(true, $"Turma {(!response.Active ? "ativada" : "inativada")} com sucesso.");
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
                string cacheKey = string.Format(_cacheKeyId, id);

                if (!_cache.TryGetValue(cacheKey, out GetClassResponse? response))
                {
                    var entity = await _classRepository.GetByIdAsync(id);
                    if (entity == null || entity?.Id == 0)
                        return new BaseResponse<GetClassResponse?>(false, "Nenhum dado encontrado.", null);

                    response = _mapper.Map<GetClassResponse>(entity);
                    _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));
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
                if (!_cache.TryGetValue(_cacheKeyList, out List<GetClassResponse>? response))
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
            return (bool)await _classRepository.ClassNameExistsAsync(className, id);
        }
        private void InvalidateCache(int? id = 0)
        {
            _cache.Remove(_cacheKeyList);

            if (id > 0)
                _cache.Remove(string.Format(_cacheKeyId, id));
        }
    }
}
