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

        public async Task<int?> CreateAsync(ClassEntity classEntity)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "INSERT INTO turma (curso_id, turma, ano, ativo) VALUES (@CourseId, @ClassName, @Year, 1); SELECT CAST(scope_identity() AS INT);";

                return await connection.ExecuteScalarAsync<int?>(sql, classEntity);
            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<List<ClassEntity>?> GetAllAsync()
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "SELECT id, curso_id AS CourseId, turma AS ClassName, ano AS Year, ativo AS Active FROM turma WITH(NOLOCK)";

                return (await connection.QueryAsync<ClassEntity>(sql)).ToList();
            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<ClassEntity?> GetByIdAsync(int id)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "SELECT id, curso_id AS CourseId, turma AS ClassName, ano AS Year, ativo AS Active FROM turma WITH(NOLOCK) WHERE id = @Id";

                return await connection.QueryFirstOrDefaultAsync<ClassEntity?>(sql, new { Id = id });

            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<bool?> UpdateAsync(ClassEntity classEntity)
        {
            try
            {

                using var connection = await _context.CreateConnectionAsync();

                string sql = "UPDATE turma SET turma = @ClassName, curso_id = @CourseId, ano = @Year, ativo = @Active WHERE id = @Id";

                return (await connection.ExecuteAsync(sql, classEntity)) > 0;
            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<bool?> UpdateActiveAsync(int id, bool active)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "UPDATE turma SET ativo = @Active WHERE id = @Id";

                return (await connection.ExecuteAsync(sql, new { Id = id, Active = active })) > 0;

            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<bool?> ClassNameExistsAsync(string className, int? id = 0)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "SELECT COUNT(1) FROM turma WITH(NOLOCK) WHERE LOWER(turma) = LOWER(@ClassName) AND id != ISNULL(@Id,0)";

                int exists = await connection.QueryFirstOrDefaultAsync<int>(sql, new { ClassName = className, Id = id });
                return exists > 0;
            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }
    }
}
