using SignalRChatTemp.AppInterfaceAdapters.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SignalRChatTemp.AppInterfaceAdapters
{
    /// <summary>
    /// 定義API回傳的JSON格式
    /// </summary>
    public class APIResponceAdapter : IAPIResponceAdapter
    {
        /// <summary>
        /// 回傳成功
        /// </summary>
        /// <param name="data">回傳資料</param>
        /// <returns></returns>
        public ObjectResult Ok(object data)
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
        public ObjectResult Fail(object data)
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
        public ObjectResult ServerFail(object data)
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
