using AutoMapper;
using Azure;
using Azure.Core;
using FIAP.GestaoEscolar.Domain.Requests.StudentClass;
using FIAP.GestaoEscolar.Domain.Responses.Base;
using FIAP.GestaoEscolar.Domain.Responses.StudentClass;
using FIAP.GestaoEscolar.Infrastructure.Entities;
using FIAP.GestaoEscolar.Infrastructure.Repositories;
using FIAP.GestaoEscolar.Infrastructure.Repositories.Implementations;
using Microsoft.Extensions.Caching.Memory;

namespace FIAP.GestaoEscolar.Application.Services.Implementations
{
    public class StudentClassService : IStudentClassService
    {
        private readonly IStudentClassRepository _studentClassRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        const string _cacheKeyListStudentsByClassId = "studentsByClassIdList-{0}";
        const string _cacheKeyListClassesByStudentId = "classesByStudentIdList-{0}";
        const string _cacheKeyId = "studentClass-{0}-{1}";
        public StudentClassService(IStudentClassRepository studentClassRepository,
            IStudentRepository studentRepository,
            IClassRepository classRepository,
            IMemoryCache cache,
            IMapper mapper)
        {
            _studentClassRepository = studentClassRepository;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<BaseResponse> CreateAsync(CreateStudentClassRequest request)
        {
            try
            {
                var entity = await _studentClassRepository.GetByIdAsync(request.StudentId, request.ClassId);
                if (entity != null)
                    return new BaseResponse(false, "Este aluno já está vinculado a esta turma");

                var dataIsValid = await ValidateStudentClass(request.StudentId, request.ClassId);
                if (!dataIsValid.Success)
                    return dataIsValid;

                var entityMapper = _mapper.Map<StudentClass>(request);
                bool? inserted = await _studentClassRepository.CreateAsync(entityMapper);

                if (inserted == null || !(bool)inserted)
                    return new BaseResponse(false, "Não foi possível vincular o aluno à turma.");

                InvalidateCache();

                return new BaseResponse(true, "Aluno vinculado à turma com sucesso.");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message);
            }
        }

        public async Task<BaseResponse> UpdateActiveAsync(UpdateStudentClassActiveRequest request)
        {
            try
            {
                var entity = await _studentClassRepository.GetByIdAsync(request.StudentId, request.ClassId);
                if (entity == null)
                    return new BaseResponse(false, "Este aluno não está vinculado a esta turma.");

                if (entity.Active == request.Active)
                    return new BaseResponse(true, $"Esse aluno já está {(request.Active ? "ativo" : "inativo")} nesta turma.");

                entity.Active = request.Active;

                bool? updated = await _studentClassRepository.UpdateActiveAsync(entity);

                if (updated == null || (bool)!updated)
                    return new BaseResponse(false, "Não foi possível atualizar a turma desse aluno.");

                InvalidateCache(request.StudentId, request.ClassId);

                return new BaseResponse(true, $"Aluno {(request.Active ? "ativado" : "inativado")} nesta turma com sucesso.");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message);
            }
        }

        public async Task<BaseResponse> UpdateAsync(UpdateStudentClassRequest request)
        {
            try
            {
                var classesByStudentId = await _studentClassRepository.GetStudentClassByStudentIdAsync(request.StudentId);

                var entity = classesByStudentId?.Where(x => x.ClassId == request.CurrentClassId).FirstOrDefault();
                if (entity == null)
                    return new BaseResponse(false, "Este aluno não está vinculado a esta turma.");

                var existNeWClass = classesByStudentId?.Any(x => x.ClassId == request.ClassId);
                if (existNeWClass != null && (bool)existNeWClass)
                    return new BaseResponse(false, "Este aluno já está vinculado a esta turma");

                var dataIsValid = await ValidateStudentClass(request.StudentId, request.ClassId);
                if (!dataIsValid.Success)
                    return dataIsValid;

                var entityMapper = _mapper.Map(request, entity);
                bool? updated = await _studentClassRepository.UpdateAsync(entityMapper, request.CurrentClassId);

                if (updated == null || (bool)!updated)
                    return new BaseResponse(false, "Não foi possível atualizar a turma desse aluno.");

                InvalidateCache(request.StudentId, request.CurrentClassId);

                return new BaseResponse(true, "Aluno vinculado à turma com sucesso.");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message);
            }
        }

        public async Task<BaseResponse<GetStudentClassResponse?>> GetByIdAsync(int studentId, int classId)
        {
            try
            {
                string cacheKey = string.Format(_cacheKeyId, studentId, classId);

                if (!_cache.TryGetValue(cacheKey, out GetStudentClassResponse? response))
                {
                    var entity = await _studentClassRepository.GetByIdAsync(studentId, classId);
                    if (entity == null)
                        return new BaseResponse<GetStudentClassResponse?>(false, "Nenhum dado encontrado.", null);

                    response = _mapper.Map<GetStudentClassResponse>(entity);
                    _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));
                }

                return new BaseResponse<GetStudentClassResponse?>(true, "Dado encontrado com sucesso.", response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetStudentClassResponse?>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse<List<GetStudentsByClassIdResponse>>> GetStudentsByClassIdAsync(int classId)
        {
            try
            {
                string cacheKey = string.Format(_cacheKeyListStudentsByClassId, classId);

                if (!_cache.TryGetValue(cacheKey, out List<GetStudentsByClassIdResponse>? response))
                {
                    var entities = await _studentClassRepository.GetStudentsByClassIdAsync(classId);
                    if (entities == null || entities.Count == 0)
                        return new BaseResponse<List<GetStudentsByClassIdResponse>>(false, "Nenhum dado encontrado.", null);

                    response = _mapper.Map<List<GetStudentsByClassIdResponse>>(entities);
                    _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));
                }

                return new BaseResponse<List<GetStudentsByClassIdResponse>>(true, "Dados encontrados com sucesso.", response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<GetStudentsByClassIdResponse>>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse<List<GetClassesByStudentIdResponse>>> GetClassesByStudentIdAsync(int studentId)
        {
            try
            {
                string cacheKey = string.Format(_cacheKeyListClassesByStudentId, studentId);

                if (!_cache.TryGetValue(cacheKey, out List<GetClassesByStudentIdResponse>? response))
                {
                    var entities = await _studentClassRepository.GetClassesByStudentIdAsync(studentId);
                    if (entities == null || entities.Count == 0)
                        return new BaseResponse<List<GetClassesByStudentIdResponse>>(false, "Nenhum dado encontrado.", null);

                    response = _mapper.Map<List<GetClassesByStudentIdResponse>>(entities);
                    _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));
                }

                return new BaseResponse<List<GetClassesByStudentIdResponse>>(true, "Dados encontrados com sucesso.", response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<GetClassesByStudentIdResponse>>(false, ex.Message, null);
            }
        }

        private void InvalidateCache(int? studentId = 0, int? classId = 0)
        {
            if (studentId > 0)
                _cache.Remove(string.Format(_cacheKeyListClassesByStudentId, studentId));

            if (classId > 0)
                _cache.Remove(string.Format(_cacheKeyListStudentsByClassId, classId));

            if (studentId > 0 && classId > 0)
                _cache.Remove(string.Format(_cacheKeyId, studentId, classId));
        }

        private async Task<BaseResponse> ValidateStudentClass(int studentId, int classId)
        {
            var studentEntity = await _studentRepository.GetByIdAsync(studentId);
            if (studentEntity == null || (studentEntity != null && !studentEntity.Active))
                return new BaseResponse(false, "Aluno não encontrado ou encontra-se inativo.");

            var classEntity = await _classRepository.GetByIdAsync(classId);
            if (classEntity == null || (classEntity != null && !classEntity.Active))
                return new BaseResponse(false, "Turma não encontrada ou encontra-se inativa.");

            return new BaseResponse(true, "Dados válidos");
        }

    }
}
