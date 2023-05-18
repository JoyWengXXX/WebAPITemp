using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommomLibrary.Authorization;
using WebAPITemplete.Services.interfaces;
using WebAPITemplete.Models.DTOs.DefaultDB;
using System.IdentityModel.Tokens.Jwt;
using WebAPITemplete.AppInterfaceAdapters.interfaces;

namespace WebAPITemplete.Controllers
{
    public class LogInController : ControllerBase
    {
        private readonly JwtHelper _jwtHelpers;
        private readonly ILogInService _logInService;
        private readonly IAPIResponceAdapter _aPIResponceAdapter;

        public LogInController(JwtHelper jwtHelpers, ILogInService logInService, IAPIResponceAdapter aPIResponceAdapter)
        {
            _jwtHelpers = jwtHelpers;
            _logInService = logInService;
            _aPIResponceAdapter = aPIResponceAdapter;
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
            UserDTO User = await _logInService.LogIn(UserID, Password);
            if (User == null)
                return BadRequest("帳號或密碼錯誤");
            string Token = _jwtHelpers.GenerateToken(User.SerialNum, User.UserID, User.RoleID);
            return _aPIResponceAdapter.Ok(Token);
        }
    }
}
