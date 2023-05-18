using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommomLibrary.Authorization;
using WebAPITemplete.Services.interfaces;
using WebAPITemplete.Models.DTOs.DefaultDB;
using System.IdentityModel.Tokens.Jwt;
using WebAPITemplete.AppInterfaceAdapters.interfaces;
using WebAPITemplete.Models.ViewModels;

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
            int RoleID = int.Parse(User.Claims.Where(x => x.Type == "role").FirstOrDefault(x => x.Type == "role").Value);
            List<MenuTreeViewModel> MenuPages = await _pageService.GetMenuPages(RoleID);
            return _aPIResponceAdapter.Ok(MenuPages);
        }
    }
}
