using webAPITemp.Models.Entities.DefaultDB;
using webAPITemp.Models.ViewModels;

namespace webAPITemp.Services.interfaces
{
    public interface IPageService
    {
        Task<List<MenuTreeViewModel>> GetMenuPages(int RoleID);
    }
}