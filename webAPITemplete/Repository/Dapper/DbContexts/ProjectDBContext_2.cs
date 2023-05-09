using Microsoft.Data.SqlClient;
using System.Data;
using webAPITemplete.Repository.Dapper.DbContexts.interfaces;

namespace webAPITemplete.Repository.Dapper.DbContexts
{
    public class ProjectDBContext_2 : IProjectDBContext_2
    {
        private readonly string _connectionString;

        public ProjectDBContext_2(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
