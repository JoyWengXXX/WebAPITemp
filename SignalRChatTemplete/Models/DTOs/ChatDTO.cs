using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SignalRChatTemplete.Models.DTOs
{
    /// <summary>
    /// 定義SignalR Hub回傳JSON格式
    /// </summary>
    public class SendMessageDTO
    {
        /// <summary>
        /// 用戶ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 傳送訊息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 傳送訊息至群組
        /// </summary>
        public string? ToGroup { get; set; }
        /// <summary>
        /// 傳送訊息至某用戶
        /// </summary>
        public string? ToUserID { get; set; }
    }
}
