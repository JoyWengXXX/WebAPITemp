using WebAPITemplete.Models.DTOs.DefaultDB;

namespace WebAPITemplete.Services.interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUserInfo(string UserID, string Password);
    }
}