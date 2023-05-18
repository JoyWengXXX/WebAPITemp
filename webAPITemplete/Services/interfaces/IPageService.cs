using WebAPITemplete.Models.Entities.DefaultDB;
using WebAPITemplete.Models.ViewModels;

namespace WebAPITemplete.Services.interfaces
{
    public interface IPageService
    {
        Task<List<MenuTreeViewModel>> GetMenuPages(int RoleID);
    }
}