using SignalRTemplete.Models.DTOs.DefaultDB;

namespace SignalRChatTemp.Services.interfaces
{
    public interface IUserInfoService
    {
        Task<UserInfoDTO> GetUserInfo(string UserID, string Password);
    }
}