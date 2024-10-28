using Dapper;
using FIAP.GestaoEscolar.Infrastructure.Contexts;
using FIAP.GestaoEscolar.Infrastructure.Entities;
using FIAP.GestaoEscolar.Infrastructure.Models.StudentClass;

namespace FIAP.GestaoEscolar.Infrastructure.Repositories.Implementations
{
    public class StudentClassRepository : IStudentClassRepository
    {
        private readonly DBContext _context;

        public StudentClassRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<bool?> CreateAsync(StudentClassEntity studentClassEntity)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "INSERT INTO aluno_turma (aluno_id, turma_id, ativo) VALUES (@StudentId, @ClassId, 1); SELECT CAST(scope_identity() AS INT);";

                return (await connection.ExecuteAsync(sql, studentClassEntity)) > 0;
            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<StudentClassEntity?> GetByIdAsync(int studentId, int classId)
        {
            try
            {
                StudentClassEntity? studentClass = null;

                using var connection = await _context.CreateConnectionAsync();

                string sql = @"SELECT 
                                    a.id AS StudentId,
                                    a.nome AS Name, 
                                    a.usuario AS Username,
                                    t.id AS ClassId,
                                    t.turma AS ClassName,
                                    t.curso_id AS CourseId,
                                    t.ano AS Year,
                                    at.ativo AS Active
                                FROM 
                                    aluno_turma at WITH(NOLOCK)
                                INNER JOIN 
                                    aluno a WITH(NOLOCK) ON a.id = at.aluno_id 
                                INNER JOIN 
                                    turma t WITH(NOLOCK) ON t.id = at.turma_id
                                WHERE at.aluno_id = @StudentId AND
                                      at.turma_id = @ClassId";

                var result = await connection.QueryFirstOrDefaultAsync(sql, new { StudentId = studentId, ClassId = classId });

                if (result != null)
                {
                    studentClass = new StudentClassEntity
                    {
                        StudentId = result.StudentId,
                        ClassId = result.ClassId,
                        Active = result.Active,
                        Student = new StudentEntity
                        {
                            Id = result.StudentId,
                            Name = result.Name,
                            Username = result.Username
                        },
                        Class = new ClassEntity
                        {
                            Id = result.ClassId,
                            ClassName = result.ClassName,
                            CourseId = result.CourseId,
                            Year = result.Year
                        }
                    };
                }

                return studentClass;
            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<List<StudentClassEntity>> GetStudentClassByStudentIdAsync(int studentId)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "SELECT aluno_id AS StudentId, turma_id AS ClassId, ativo AS Active FROM aluno_turma WITH(NOLOCK) WHERE aluno_id = @StudentId";

                return (await connection.QueryAsync<StudentClassEntity>(sql, new { StudentId = studentId })).ToList();

            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<List<StudentsByClassIdModel>> GetStudentsByClassIdAsync(int classId)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = @"SELECT 
                                t.id AS ClassId,
                                t.turma AS ClassName,
                                t.ano AS Year,
                                t.curso_id AS CourseId,
                                t.ativo AS Active,
                                a.id AS StudentId,
                                a.nome AS Name, 
                                a.usuario AS Username,
                                at.ativo AS StudentClassActive
                            FROM 
                                aluno_turma at WITH(NOLOCK)
                            INNER JOIN 
                                aluno a WITH(NOLOCK) ON a.id = at.aluno_id 
                            INNER JOIN 
                                turma t WITH(NOLOCK) ON t.id = at.turma_id
                            WHERE 
                                t.id = @ClassId";

                var results = await connection.QueryAsync(sql, new { ClassId = classId});

                var studentsByClass = results
                    .GroupBy(r => new
                    {
                        ClassId = (int)r.ClassId,
                        ClassName = (string)r.ClassName,
                        CourseId = (int)r.CourseId,
                        Year = (int)r.Year,
                        Active = (bool)r.Active
                    })
                    .Select(g => new StudentsByClassIdModel
                    {
                        Id = g.Key.ClassId,
                        ClassName = g.Key.ClassName,
                        CourseId = g.Key.CourseId,
                        Year = g.Key.Year,
                        Active = g.Key.Active,
                        Students = g.Select(x => new StudentEntity
                        {
                            Id = (int)x.StudentId,
                            Name = (string)x.Name,
                            Username = (string)x.Username,
                            Active = (bool)x.StudentClassActive
                        }).ToList()
                    })
                    .ToList();

                return studentsByClass;
            }
            catch (Exception)
            {
                //Insere log
                return null;
            }

        }

        public async Task<List<ClassesByStudentIdModel>> GetClassesByStudentIdAsync(int studentId)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = @"SELECT 
                                a.id AS StudentId,
                                a.nome AS Name, 
                                a.usuario AS Username,
                                a.ativo AS Active,
                                t.id AS ClassId,
                                t.turma AS ClassName,
                                t.ano AS Year,
                                t.curso_id AS CourseId,
                                at.ativo AS StudentClassActive
                            FROM 
                                aluno_turma at WITH(NOLOCK)
                            INNER JOIN 
                                aluno a WITH(NOLOCK) ON a.id = at.aluno_id 
                            INNER JOIN 
                                turma t WITH(NOLOCK) ON t.id = at.turma_id
                            WHERE 
                                a.id = @StudentId";

                var results = await connection.QueryAsync(sql, new { StudentId = studentId });

                var classesByStudent = results
                    .GroupBy(x=> new
                    {
                        Id = (int)x.StudentId,
                        Name = (string)x.Name,
                        Username = (string)x.Username,
                        Active = (bool)x.Active
                    })
                    .Select(g => new ClassesByStudentIdModel
                    {
                        Id = (int)g.Key.Id,
                        Name = (string)g.Key.Name,
                        Username = (string)g.Key.Username,
                        Active = (bool)g.Key.Active,
                        Classes = g.Select(r => new ClassEntity
                        {
                            Id = (int)r.ClassId,
                            ClassName = (string)r.ClassName,
                            CourseId = (int)r.CourseId,
                            Year = (int)r.Year,
                            Active = (bool)r.StudentClassActive
                        }).ToList()
                    })
                    .ToList();

                return classesByStudent;
            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<bool?> UpdateActiveAsync(StudentClassEntity studentClassEntity)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "UPDATE aluno_turma SET ativo = @Active WHERE aluno_id = @StudentId AND turma_id = @ClassId";

                return (await connection.ExecuteAsync(sql, studentClassEntity)) > 0;

            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<bool?> UpdateAsync(StudentClassEntity studentClassEntity, int currentClassId)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "UPDATE aluno_turma SET turma_id = @ClassId, ativo = @Active WHERE aluno_id = @StudentId AND turma_id = @CurrentClassId";

                return (await connection.ExecuteAsync(sql, new { studentClassEntity.ClassId, studentClassEntity.Active, studentClassEntity.StudentId, CurrentClassId = currentClassId })) > 0;
            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }
    }
}
