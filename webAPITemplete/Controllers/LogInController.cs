using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommomLibrary.Authorization;

namespace WebAPITemplete.Controllers
{
    public class LogInController : ControllerBase
    {
        private readonly JwtHelper _jwtHelpers;

        public LogInController(JwtHelper jwtHelpers)
        {
            _jwtHelpers = jwtHelpers;
        }

        /// <summary>
        /// 取得TOKEN
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("Login"), AllowAnonymous]
        public ActionResult<string> Login(string username, string password)
        {
            var token = _jwtHelpers.GenerateToken(username);
            return Ok(token);
        }

        [HttpGet("username"), Authorize(Roles = "admin")]
        public ActionResult<string> Username()
        {
            return Ok(User.Identity?.Name);
        }
    }
}
