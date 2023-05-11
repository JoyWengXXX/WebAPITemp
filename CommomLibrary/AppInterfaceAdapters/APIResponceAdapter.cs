using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CommomLibrary.AppInterfaceAdapters
{
    /// <summary>
    /// 定義API回傳的JSON格式
    /// </summary>
    public class APIResponceAdapter
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

    /// <summary>
    /// 定義SignalR Hub回傳JSON格式
    /// </summary>
    public class HubReponseAdapter
    {
        /// <summary>
        /// 流水編號
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用戶ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 傳送訊息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 用戶所屬群組
        /// </summary>
        public string Group { get; set; }
    }
}
