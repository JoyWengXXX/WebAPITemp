using webAPITemp.Models.DTOs.DefaultDB;
using webAPITemp.Models.ViewModels;

namespace webAPITemp.Services.interfaces
{
    public interface ILogInService
    {
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Task<UserDTO> LogIn(string UserID, string Password);

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public Task LogOut();
    }
}
