using Microsoft.Data.SqlClient;
using System.Data;
using webAPITemplete.Repository.Dapper.DbContexts.interfaces;

namespace webAPITemplete.Repository.Dapper.DbContexts
{
    public class ProjectDBContext_Default : IProjectDBContext
    {
        private readonly string _connectionString;

        public ProjectDBContext_Default(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
