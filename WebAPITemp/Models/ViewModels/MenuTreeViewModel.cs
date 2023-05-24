using webAPITemp.Models.DTOs.DefaultDB;

namespace webAPITemp.Models.ViewModels
{
    public class MenuTreeViewModel : PageDTO
    {
        /// <summary>
        /// 子頁面
        /// </summary>
        public List<MenuTreeViewModel> SubPages { get; set; } = new List<MenuTreeViewModel>();
    }
}
