using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommomLibrary.Authorization;
using SignalRChatTemplete.Services.interfaces;
using System.IdentityModel.Tokens.Jwt;
using SignalRTemplete.Models.DTOs.DefaultDB;

namespace SignalRChatTemplete.Controllers
{
    public class LogInController : ControllerBase
    {
        private readonly JwtHelper _jwtHelpers;
        private readonly IUserInfoService _userInfoService;

        public LogInController(JwtHelper jwtHelpers, IUserInfoService userInfoService)
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
        [HttpPost("Login"), AllowAnonymous]
        public async Task<IActionResult> Login(string UserID, string Password)
        {
            UserInfoDTO userInfo = await _userInfoService.GetUserInfo(UserID, Password);
            if (userInfo == null)
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
