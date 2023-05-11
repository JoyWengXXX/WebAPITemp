using CommomLibrary.AppInterfaceAdapters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CommomLibrary.MiddleWares
{
    /// <summary>
    /// 處理Exception發生時的步驟
    /// 掛在主專案的MiddleWare中
    /// </summary>
    public class ErrorHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandler> logger;

        public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                //記錄錯誤Log
                logger.LogError(error, error.Message);

                var response = context.Response;
                response.ContentType = "application/json";
                ObjectResult result;
                //如果是自定義的錯誤，則回傳客製化的錯誤訊息
                if (error is ErrorHandling.CustomExceptions.AppException)
                {
                    result = APIResponceAdapter.Fail(error.Message);
                }
                else
                {
                    result = APIResponceAdapter.ServerFail("伺服器錯誤");
                }
                await response.WriteAsync(JsonConvert.SerializeObject(result.Value));
            }
        }
    }
}