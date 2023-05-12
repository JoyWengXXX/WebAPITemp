using CommomLibrary.Dapper.Repository.interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SignalRChatTemplete.DBContexts.Dapper
{
    public class ProjectDBContext : IProjectDBContext
    {
        private readonly string _connectionString;

        public ProjectDBContext(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
