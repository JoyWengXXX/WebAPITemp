using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommomLibrary.Authorization;
using webAPITemp.Services.interfaces;
using webAPITemp.Models.DTOs.DefaultDB;
using System.IdentityModel.Tokens.Jwt;
using webAPITemp.AppInterfaceAdapters.interfaces;

namespace webAPITemp.Controllers
{
    public class LogInController : ControllerBase
    {
        private readonly JwtHelper _jwtHelpers;
        private readonly ILogger<LogInController> _logger;
        private readonly ILogInService _logInService;
        private readonly IAPIResponceAdapter _aPIResponceAdapter;

        public LogInController(JwtHelper jwtHelpers,  ILogInService logInService, IAPIResponceAdapter aPIResponceAdapter, ILogger<LogInController> logger)
        {
            _jwtHelpers = jwtHelpers;
            _logInService = logInService;
            _aPIResponceAdapter = aPIResponceAdapter;
            _logger = logger;
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
            string Token = _jwtHelpers.CreateToken(new JwtTokenOptions { UserSerialNum = User.SerialNum, UserName = User.UserID, RoleID = User.RoleID });
            //紀錄Log
            _logger.LogInformation($"User:{User.UserID} Login");
            return _aPIResponceAdapter.Ok(Token);
        }
    }
}
