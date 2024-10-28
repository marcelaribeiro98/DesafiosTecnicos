using AutoMapper;
using FIAP.GestaoEscolar.Application.Helpers;
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

        public async Task<BaseResponse<int?>> CreateAsync(CreateStudentRequest request)
        {
            try
            {
                var entityMapper = _mapper.Map<StudentEntity>(request);

                string password = entityMapper.Password;
                string hashPassword = Cryptography.HashPassword(password);

                entityMapper.Password = hashPassword;

                int? id = await _studentRepository.CreateAsync(entityMapper);

                if (id == null || id == 0)
                    return new BaseResponse<int?>(false, "Não foi possível cadastrar aluno.");

                InvalidateCache();

                return new BaseResponse<int?>(true, "Aluno cadastrado com sucesso.", id);
            }
            catch (Exception ex)
            {
                return new BaseResponse<int?>(false, ex.Message);
            }
        }

        public async Task<BaseResponse> UpdateAsync(UpdateStudentRequest request)
        {
            try
            {
                var entity = await _studentRepository.GetByIdAsync(request.Id);
                if (entity == null)
                    return new BaseResponse(false, "Aluno não encontrado.");

                string currentHashPassword = entity.Password;

                var entityMapper = _mapper.Map(request, entity);

                //atualiza a senha
                if (!string.IsNullOrEmpty(request.Password) && !Cryptography.VerifyPassword(currentHashPassword, request.Password))
                {
                    string hashPassword = Cryptography.HashPassword(request.Password);
                    entityMapper.Password = hashPassword;
                }
                else //mantem a senha
                    entityMapper.Password = currentHashPassword;

                bool? updated = await _studentRepository.UpdateAsync(entityMapper);

                if (updated == null || (bool)!updated)
                    return new BaseResponse(false, "Não foi possível atualizar aluno.");

                InvalidateCache(request.Id);

                return new BaseResponse(true, "Aluno atualizado com sucesso.");
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
                    return new BaseResponse(false, "Aluno não encontrado.");

                bool? updated = await _studentRepository.UpdateActiveAsync(id, !response.Active);

                if (updated == null || (bool)!updated)
                    return new BaseResponse(false, $"Não foi possível atualizar aluno.");

                InvalidateCache(id);

                return new BaseResponse(true, $"Aluno {(!response.Active ? "ativado" : "inativado")} com sucesso.");
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
                string cacheKey = string.Format(_cacheKeyId, id);

                if (!_cache.TryGetValue(cacheKey, out GetStudentResponse? response))
                {
                    var entity = await _studentRepository.GetByIdAsync(id);
                    if (entity == null || entity?.Id == 0)
                        return new BaseResponse<GetStudentResponse?>(false, "Nenhum dado encontrado.", null);

                    response = _mapper.Map<GetStudentResponse>(entity);
                    _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));
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
                if (!_cache.TryGetValue(_cacheKeyList, out List<GetStudentResponse>? response))
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
            return (bool)await _studentRepository.UserNameExistsAsync(username, id);
        }

        private void InvalidateCache(int? id = 0)
        {
            _cache.Remove(_cacheKeyList);

            if (id > 0)
                _cache.Remove(string.Format(_cacheKeyId, id));
        }

    }
}
