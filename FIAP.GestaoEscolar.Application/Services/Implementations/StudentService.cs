using AutoMapper;
using FIAP.GestaoEscolar.Domain.Requests.Student;
using FIAP.GestaoEscolar.Domain.Responses.Base;
using FIAP.GestaoEscolar.Domain.Responses.Student;
using FIAP.GestaoEscolar.Infrastructure.Entities;
using FIAP.GestaoEscolar.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace FIAP.GestaoEscolar.Application.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        const string _cacheKeyList = "studentList";
        const string _cacheKeyId = "student-{0}";

        public StudentService(IStudentRepository studentRepository, IMemoryCache cache, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<BaseResponse<int>> CreateAsync(CreateStudentRequest request)
        {
            try
            {
                var entityMapper = _mapper.Map<Student>(request);
                int id = await _studentRepository.CreateAsync(entityMapper);

                if (id == 0)
                    return new BaseResponse<int>(false, "Não foi possível cadastrar aluno.", 0);

                _cache.Remove(_cacheKeyList);

                return new BaseResponse<int>(true, "Aluno cadastrado com sucesso.", id);
            }
            catch (Exception ex)
            {
                return new BaseResponse<int>(false, ex.Message, 0);
            }
        }

        public async Task<BaseResponse> UpdateAsync(UpdateStudentRequest request)
        {
            try
            {
                var entity = await _studentRepository.GetByIdAsync(request.Id);
                if (entity == null)
                    return new BaseResponse(false, "Aluno não encontrado.");

                var entityMapper = _mapper.Map(request, entity);

                bool updated = await _studentRepository.UpdateAsync(entityMapper);

                if (!updated)
                    return new BaseResponse(false, "Não foi possível atualizar aluno.");

                _cache.Remove(_cacheKeyList);
                _cache.Remove(string.Format(_cacheKeyId, request.Id));

                return new BaseResponse(true, "Aluno atualizado com sucesso.");
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
                string menssageSuccess = $"Aluno {(!active ? "inativado" : "ativado")} com sucesso";

                var response = (await GetByIdAsync(id)).Data;
                if (response == null)
                    return new BaseResponse(false, "Aluno não encontrado.");

                if (response.Active == active)
                    return new BaseResponse(true, menssageSuccess);

                bool updated = await _studentRepository.UpdateActiveAsync(id, active);

                if (!updated)
                    return new BaseResponse(false, $"Não foi possível atualizar aluno.");

                _cache.Remove(_cacheKeyList);
                _cache.Remove(string.Format(_cacheKeyId, id));

                return new BaseResponse(true, menssageSuccess);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message);
            }
        }

        public async Task<BaseResponse<GetStudentResponse?>> GetByIdAsync(int id)
        {
            try
            {
                string formatCacheKey = string.Format(_cacheKeyId, id);
                GetStudentResponse? response = null;

                if (!_cache.TryGetValue(formatCacheKey, out response))
                {
                    var entity = await _studentRepository.GetByIdAsync(id);

                    if (entity == null || entity?.Id == 0)
                        return new BaseResponse<GetStudentResponse?>(false, "Nenhum dado encontrado.", null);

                    response = _mapper.Map<GetStudentResponse>(entity);

                    _cache.Set(formatCacheKey, response, TimeSpan.FromMinutes(10));
                }

                return new BaseResponse<GetStudentResponse?>(true, "Dado encontrado com sucesso.", response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetStudentResponse?>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse<List<GetStudentResponse>>> GetAllAsync()
        {
            try
            {
                List<GetStudentResponse>? response = null;

                if (!_cache.TryGetValue(_cacheKeyList, out response))
                {
                    var entities = await _studentRepository.GetAllAsync();

                    if (entities == null || entities.Count == 0)
                        return new BaseResponse<List<GetStudentResponse>>(false, "Nenhum dado encontrado.", null);

                    response = _mapper.Map<List<GetStudentResponse>>(entities);

                    _cache.Set(_cacheKeyList, response, TimeSpan.FromMinutes(10));
                }

                return new BaseResponse<List<GetStudentResponse>>(true, "Dados encontrados com sucesso.", response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<GetStudentResponse>>(false, ex.Message, null);
            }
        }
        public async Task<bool> UserNameExistsAsync(string username, int? id = 0)
        {
            return (await _studentRepository.UserNameExistsAsync(username, id)) > 0;
        }
    }
}
