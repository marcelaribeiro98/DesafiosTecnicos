using Dapper;
using FIAP.GestaoEscolar.Infrastructure.Contexts;
using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Infrastructure.Repositories.Implementations
{
    public class ClassRepository : IClassRepository
    {
        private readonly DBContext _context;

        public ClassRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Class classEntity)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "INSERT INTO turma (curso_id, turma, ano, ativo) VALUES (@CourseId, @ClassName, @Year, 1); SELECT CAST(scope_identity() AS INT);";

                return await connection.ExecuteScalarAsync<int>(sql, classEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Class>> GetAllAsync()
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "SELECT id, curso_id AS CourseId, turma AS ClassName, ano AS Year, ativo AS Active FROM turma WITH(NOLOCK)";

                return (await connection.QueryAsync<Class>(sql)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Class?> GetByIdAsync(int id)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "SELECT id, curso_id AS CourseId, turma AS ClassName, ano AS Year, ativo AS Active FROM turma WITH(NOLOCK) WHERE id = @Id";

                return await connection.QueryFirstOrDefaultAsync<Class?>(sql, new { Id = id });

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateAsync(Class classEntity)
        {
            try
            {

                using var connection = await _context.CreateConnectionAsync();

                string sql = "UPDATE turma SET turma = @ClassName, curso_id = @CourseId, ano = @Year, ativo = @Active WHERE id = @Id";

                return (await connection.ExecuteAsync(sql, classEntity)) > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateActiveAsync(int id, bool active)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "UPDATE turma SET ativo = @Active WHERE id = @Id";

                return (await connection.ExecuteAsync(sql, new { Id = id, Active = active })) > 0;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> ClassNameExistsAsync(string className, int? id = 0)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "SELECT COUNT(1) FROM turma WITH(NOLOCK) WHERE LOWER(turma) = LOWER(@ClassName) AND id != ISNULL(@Id,0)";

                var resultado =  await connection.QueryFirstOrDefaultAsync<int>(sql, new { ClassName = className, Id = id });
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
