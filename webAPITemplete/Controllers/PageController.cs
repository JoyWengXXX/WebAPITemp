using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPITemplete.Services.interfaces;
using WebAPITemplete.AppInterfaceAdapters.interfaces;
using WebAPITemplete.Models.ViewModels;
using System.Security.Claims;

namespace WebAPITemplete.Controllers
{
    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;
        private readonly IAPIResponceAdapter _aPIResponceAdapter;

        public PageController(IPageService pageService, IAPIResponceAdapter aPIResponceAdapter)
        {
            _pageService = pageService;
            _aPIResponceAdapter = aPIResponceAdapter;
        }

        /// <summary>
        /// 取得對應權限的選單
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenuPages")]
        [Authorize]
        public async Task<IActionResult> GetMenuPages()
        {
            int RoleID = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value);
            List<MenuTreeViewModel> MenuPages = await _pageService.GetMenuPages(RoleID);
            return _aPIResponceAdapter.Ok(MenuPages);
        }
    }
}
