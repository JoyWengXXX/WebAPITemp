using CommomLibrary.Dapper.Repository.interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SignalRChatTemplete.DBContexts.Dapper
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
