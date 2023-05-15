using CommomLibrary.Dapper.Repository.interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SignalRChatTemplete.DBContexts.Dapper
{
    public class ProjectDBContext_SignalR : IProjectDBContext
    {
        private readonly string _connectionString;

        public ProjectDBContext_SignalR(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
