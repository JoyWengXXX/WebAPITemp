using WebAPITemplete.Models.DTOs.DefaultDB;
using WebAPITemplete.Models.ViewModels;

namespace WebAPITemplete.Services.interfaces
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
