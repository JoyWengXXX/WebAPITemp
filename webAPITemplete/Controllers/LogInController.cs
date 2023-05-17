using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommomLibrary.Authorization;
using WebAPITemplete.Services.interfaces;
using WebAPITemplete.Models.DTOs.DefaultDB;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPITemplete.Controllers
{
    public class LogInController : ControllerBase
    {
        private readonly JwtHelper _jwtHelpers;
        private readonly IUserService _userInfoService;

        public LogInController(JwtHelper jwtHelpers, IUserService userInfoService)
        {
            _jwtHelpers = jwtHelpers;
            _userInfoService = userInfoService;
        }

        /// <summary>
        /// 取得TOKEN
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Password"></param> 
        /// <returns></returns>
        [HttpGet("Login"), AllowAnonymous]
        public async Task<IActionResult> Login(string UserID, string Password)
        {
            UserDTO userInfo = await _userInfoService.GetUserInfo(UserID, Password);
            if(userInfo == null)
                return BadRequest("帳號或密碼錯誤");
            string token = _jwtHelpers.GenerateToken(userInfo.SerialNum, userInfo.UserID, userInfo.RoleName);
            return Ok(token);
        }

        [HttpGet("Username")]
        [Authorize(Roles = "Admin,User")]
        public ActionResult<string> Username()
        {
            return Ok(User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).Select(x => x.Value).FirstOrDefault());
        }
    }
}
