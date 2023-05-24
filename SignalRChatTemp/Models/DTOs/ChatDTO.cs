using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SignalRChatTemp.Models.DTOs
{
    /// <summary>
    /// 傳送訊息
    /// </summary>
    public class SendMessageDTO
    {
        /// <summary>
        /// 傳送訊息
        /// </summary>
        [Required]
        public string Message { get; set; }
        /// <summary>
        /// 傳送訊息至群組ID
        /// </summary>
        public int? ToGroupID { get; set; }
        /// <summary>
        /// 傳送訊息至某用戶
        /// </summary>
        public int? ToUserID { get; set; }
    }

    /// <summary>
    /// 回傳
    /// </summary>
    public class ResponceDTO
    {
        /// <summary>
        /// 用戶ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 用戶名稱
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 傳送訊息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 傳送時間
        /// </summary>
        public DateTime SendTime { get; set; }
    }
}
