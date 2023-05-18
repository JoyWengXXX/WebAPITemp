using CommomLibrary.Dapper.Repository.interfaces;
using Dapper;
using System.Data;
using WebAPITemplete.DBContexts.Dapper;
using WebAPITemplete.Models.DTOs.DefaultDB;
using WebAPITemplete.Models.Entities.DefaultDB;
using WebAPITemplete.Models.ViewModels;
using WebAPITemplete.Services.interfaces;

namespace WebAPITemplete.Services
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
                        WHERE U.UserID = @UserID AND U.Password = @Password AND U.IsEnable = 1";
            return await _dbConnection.QuerySingleOrDefaultAsync<UserDTO>(sql, new { UserID, Password });
        }

        public async Task LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
