using Microsoft.Data.SqlClient;
using System.Data;
using webAPITemplete.Repository.Dapper.DbContexts.interfaces;

namespace webAPITemplete.Repository.Dapper.DbContexts
{
    public class ProjectDBContext_3 : IProjectDBContext_3
    {
        private readonly string _connectionString;

        public ProjectDBContext_3(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
