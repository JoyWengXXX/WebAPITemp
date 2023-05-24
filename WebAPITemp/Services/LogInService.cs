using CommomLibrary.Dapper.Repository.interfaces;
using Dapper;
using System.Data;
using webAPITemp.DBContexts.Dapper;
using webAPITemp.Models.DTOs.DefaultDB;
using webAPITemp.Models.Entities.DefaultDB;
using webAPITemp.Models.ViewModels;
using webAPITemp.Services.interfaces;

namespace webAPITemp.Services
{
    public class LogInService : ILogInService
    {
        private readonly IDbConnection _dbConnection;

        public LogInService(IBaseDapper<ProjectDBContext_Default> baseDapperDefault)
        {
            _dbConnection = baseDapperDefault.CreateConnection();
        }

        public async Task<UserDTO> LogIn(string UserID, string Password)
        {
            var sql = @"SELECT U.SerialNum,
                               U.UserID,
                               U.FirstName,
                               U.LastName,
                               R.RoleID,
                               R.RoleName
                        FROM [dbo].[User] U
                        INNER JOIN UserPasswordRecord UP ON U.SerialNum = UP.UserSerialNum AND UP.IsEnable = 1
                        LEFT JOIN Role R ON U.RoleID = R.RoleID AND R.IsEnable = 1
                        WHERE U.UserID = @UserID AND UP.Password = @Password AND U.IsEnable = 1";
            return await _dbConnection.QuerySingleOrDefaultAsync<UserDTO>(sql, new { UserID, Password });
        }

        public async Task LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
