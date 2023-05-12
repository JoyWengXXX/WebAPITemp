using CommomLibrary.Dapper.Repository.interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace webAPITemplete.DBContexts.Dapper
{
    public class ProjectDBContext_Test1 : IProjectDBContext
    {
        private readonly string _connectionString;

        public ProjectDBContext_Test1(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
