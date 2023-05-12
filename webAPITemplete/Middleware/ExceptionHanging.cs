using webAPITemplete.AppInterfaceAdapters.interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace webAPITemplete.Middleware
{
    /// <summary>
    /// 處理Exception發生時的步驟
    /// 掛在MiddleWare中
    /// </summary>
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandler> _logger;
        private readonly IAPIResponceAdapter _httpResponceAdapter;

        public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger, IAPIResponceAdapter httpResponceAdapter)
        {
            _next = next;
            _logger = logger;
            _httpResponceAdapter = httpResponceAdapter;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                //記錄錯誤Log
                _logger.LogError(error, error.Message);

                var response = context.Response;
                response.ContentType = "application/json";
                ObjectResult result;
                //如果是自定義的錯誤，則回傳客製化的錯誤訊息
                if (error is CustomExceptions.AppException)
                {
                    result = _httpResponceAdapter.Fail(error.Message);
                }
                else
                {
                    result = _httpResponceAdapter.ServerFail("伺服器錯誤");
                }
                await response.WriteAsync(JsonConvert.SerializeObject(result.Value));
            }
        }
    }


    //預設自定義Exception
    public class CustomExceptions
    {
        public class AppException : Exception
        {
            public AppException(string message) : base(message)
            {
            }
        }
    }
}