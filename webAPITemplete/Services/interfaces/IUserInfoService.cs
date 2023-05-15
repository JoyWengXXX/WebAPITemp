using WebAPITemplete.Models.DTOs.DefaultDB;

namespace WebAPITemplete.Services.interfaces
{
    public interface IUserInfoService
    {
        Task<UserInfoDTO> GetUserInfo(string UserID, string Password);
    }
}