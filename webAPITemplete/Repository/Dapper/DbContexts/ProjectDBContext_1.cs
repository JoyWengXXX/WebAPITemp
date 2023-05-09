using Microsoft.Data.SqlClient;
using System.Data;
using webAPITemplete.Repository.Dapper.DbContexts.interfaces;

namespace webAPITemplete.Repository.Dapper.DbContexts
{
    public class ProjectDBContext_1 : IProjectDBContext_1
    {
        private readonly string _connectionString;

        public ProjectDBContext_1(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
