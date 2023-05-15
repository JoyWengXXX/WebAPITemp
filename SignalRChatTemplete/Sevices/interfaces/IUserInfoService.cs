using SignalRTemplete.Models.DTOs.DefaultDB;

namespace SignalRChatTemplete.Services.interfaces
{
    public interface IUserInfoService
    {
        Task<UserInfoDTO> GetUserInfo(string UserID, string Password);
    }
}