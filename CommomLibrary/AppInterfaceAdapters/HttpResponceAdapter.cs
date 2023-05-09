using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CommomLibrary.AppInterfaceAdapters
{
    /// <summary>
    /// 定義Http回傳的JSON格式
    /// </summary>
    public class HttpResponceAdapter
    {
        /// <summary>
        /// 回傳成功
        /// </summary>
        /// <param name="data">回傳資料</param>
        /// <returns></returns>
        public static ObjectResult Ok(object data)
        {
            return new OkObjectResult(new 
            {
                HttpCode = HttpStatusCode.OK,
                HttpRespondMessage = "Success",
                HttpRespondResult = data,
            });
        }
        /// <summary>
        /// 回傳失敗
        /// </summary>
        /// <returns></returns>
        public static ObjectResult Fail(object data)
        {
            return new BadRequestObjectResult(new
            {
                HttpCode = HttpStatusCode.BadRequest,
                HttpRespondMessage = "Fail",
                HttpRespondResult = data,
            });
        }
        /// <summary>
        /// 回傳伺服器失敗
        /// </summary>
        /// <returns></returns>
        public static ObjectResult ServerFail(object data)
        {
            return new BadRequestObjectResult(new 
            { 
                HttpCode = HttpStatusCode.InternalServerError,
                HttpRespondMessage = "ServerFail",
                HttpRespondResult = data,
            });
        }
    }
}
