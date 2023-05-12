using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SignalRChatTemplete.Models.Entities
{
    /// <summary>
    /// 使用者群組
    /// </summary>
    public class UserGroupInfo
    {
        /// <summary>
        /// 群組編號
        /// </summary>
        [Key]
        [Required]
        public int GroupID { get; set; }

        /// <summary>
        /// 使用者編號
        /// </summary>
        [Key]
        [Required]
        public int UserID { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [Required]
        public bool IsValid { get; set; }

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

        /// <summary>
        /// 更新者
        /// </summary>
        public int? UpdateBy { get; set; }

        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime? UpdateOn { get; set; }
    }
}
