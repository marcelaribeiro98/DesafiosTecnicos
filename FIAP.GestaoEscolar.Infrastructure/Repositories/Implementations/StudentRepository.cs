﻿using Dapper;
using FIAP.GestaoEscolar.Infrastructure.Contexts;
using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Infrastructure.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DBContext _context;

        public StudentRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<int?> CreateAsync(StudentEntity studentEntity)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "INSERT INTO aluno (nome, usuario, senha, ativo) VALUES (@Name, @Username, TRIM(@Password), 1); SELECT CAST(scope_identity() AS INT);";

                return await connection.ExecuteScalarAsync<int>(sql, studentEntity);
            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<List<StudentEntity>?> GetAllAsync()
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "SELECT id, nome AS Name, usuario AS Username, senha AS Password, ativo AS Active FROM aluno WITH(NOLOCK)";

                return (await connection.QueryAsync<StudentEntity>(sql)).ToList();

            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<StudentEntity?> GetByIdAsync(int id)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "SELECT  id, nome AS Name, usuario AS Username, senha AS Password, ativo AS Active FROM aluno WITH(NOLOCK) WHERE id = @Id";

                return await connection.QueryFirstOrDefaultAsync<StudentEntity?>(sql, new { Id = id });
            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<bool?> UpdateAsync(StudentEntity studentEntity)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "UPDATE aluno SET nome = @Name, usuario = @Username, senha = TRIM(@Password), ativo = @Active WHERE id = @Id";

                return (await connection.ExecuteAsync(sql, studentEntity)) > 0;

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

                string sql = "UPDATE aluno SET ativo = @Active WHERE id = @Id";

                return (await connection.ExecuteAsync(sql, new { Id = id, Active = active })) > 0;
            }
            catch (Exception)
            {
                //Insere log
                return null;
            }
        }

        public async Task<bool?> UserNameExistsAsync(string username, int? id = 0)
        {
            try
            {
                using var connection = await _context.CreateConnectionAsync();

                string sql = "SELECT COUNT(1) FROM aluno WITH(NOLOCK) WHERE LOWER(usuario) = LOWER(@Username) AND id != ISNULL(@Id,0)";

                int exists = await connection.QueryFirstOrDefaultAsync<int>(sql, new { Username = username, Id = id });

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
