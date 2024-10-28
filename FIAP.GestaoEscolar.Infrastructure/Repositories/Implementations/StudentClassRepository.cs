using Dapper;
using FIAP.GestaoEscolar.Infrastructure.Contexts;
using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Infrastructure.Repositories.Implementations
{
    public class StudentClassRepository : IStudentClassRepository
    {
        private readonly DBContext _context;

        public StudentClassRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(StudentClass studentClassEntity)
        {
            using var connection = await _context.CreateConnectionAsync();

            string sql = "INSERT INTO aluno_turma (aluno_id, turma_id) VALUES (@AlunoId, @TurmaId); SELECT CAST(scope_identity() AS INT);";

            return await connection.ExecuteScalarAsync<int>(sql, studentClassEntity);
        }

        public async Task<List<StudentClass>> GetAllAsync()
        {
            using var connection = await _context.CreateConnectionAsync();

            string sql = "SELECT aluno_id AS AlunoId, turma_id AS TurmaId FROM aluno_turma WITH(NOLOCK)";

            return (await connection.QueryAsync<StudentClass>(sql)).ToList();
        }

        public async Task<bool> UpdateAsync(StudentClass studentClassEntity)
        {

            using var connection = await _context.CreateConnectionAsync();

            string sql = "UPDATE aluno_turma SET turma_id = @TurmaId WHERE aluno_id = @AlunoId";

            return (await connection.ExecuteAsync(sql, studentClassEntity)) > 0;
        }
    }
}
