using webAPITemp.Models.DTOs.DefaultDB;
using webAPITemp.Models.Entities.DefaultDB;

namespace webAPITemp.Services.interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// 取得使用者清單
        /// </summary>
        /// <returns></returns>
        Task<List<UserDTO>> GetUsers();

        /// <summary>
        /// 取得個別使用者
        /// </summary>
        /// <param name="UserSerialNum"></param>
        /// <returns></returns>
        Task<UserDTO> GetUser(int UserSerialNum);

        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        Task CreateUser(User User);

        /// <summary>
        /// 更新使用者
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        Task UpdateUser(User User);
        
        /// <summary>
        /// 刪除使用者
        /// </summary>
        /// <param name="UserSerialNum"></param>
        /// <returns></returns>
        Task DeleteUser(int UserSerialNum);
    }
}