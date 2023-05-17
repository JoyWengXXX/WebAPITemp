using CommomLibrary.Dapper.Repository.interfaces;
using Dapper;
using System.Data;
using WebAPITemplete.DBContexts.Dapper;
using WebAPITemplete.Models.DTOs.DefaultDB;
using WebAPITemplete.Services.interfaces;

namespace WebAPITemplete.Services
{
    public class UserService : IUserService
    {
        private readonly IDbConnection _dbConnection;

        public UserService(IBaseDapper<ProjectDBContext_Default> baseDapperDefault)
        {
            _dbConnection = baseDapperDefault.CreateConnection();
        }

        /// <summary>
        /// 查詢使用者資訊
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<UserDTO> GetUserInfo(string UserID, string Password)
        {
            var sql = @"SELECT U.SerialNum,
                               U.UserID,
                               U.FirstName,
                               U.LastName,
                               R.RoleID,
                               R.RoleName
                        FROM UserInfo U
                        INNER JOIN UserPasswordRecord UP ON U.SerialNum = UP.UserInfoSerialNum AND UP.IsEnable = 1
                        LEFT JOIN RoleInfo R ON U.RoleID = R.RoleID AND R.IsEnable = 1
                        WHERE U.UserID = @UserID AND UP.Password = @Password AND U.IsEnable = 1";
            return await _dbConnection.QuerySingleOrDefaultAsync<UserDTO>(sql, new { UserID = UserID, Password = Password });
        }
    }
}
