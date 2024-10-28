using Microsoft.Data.SqlClient;
using System.Data;

namespace FIAP.GestaoEscolar.Infrastructure.Contexts
{
    public class DBContext
    {
        private readonly string _connectionString;

        public DBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}