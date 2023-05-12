using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SignalRChatTemplete.Models.Entities
{
    /// <summary>
    /// 聊天紀錄
    /// </summary>
    public class ChatRecord
    {
        /// <summary>
        /// 流水編號
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerialID { get; set; }

        /// <summary>
        /// 群組編號
        /// </summary>
        [Required]
        public int GroupID { get; set; }

        /// <summary>
        /// 使用者編號
        /// </summary>
        [Required]
        public int UserID { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [StringLength(5000)]
        public string Text { get; set; }

        /// <summary>
        /// 建立者
        /// </summary>
        [Required]
        public int CreateBy { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Required]
        public DateTime CreateOn { get; set; }
    }
}
