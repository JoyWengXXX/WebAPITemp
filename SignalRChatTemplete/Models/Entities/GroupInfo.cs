using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SignalRChatTemplete.Models.Entities
{
    /// <summary>
    /// 群組資料
    /// </summary>
    public class GroupInfo
    {
        /// <summary>
        /// 群組編號
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupID { get; set; }

        /// <summary>
        /// 群組名稱
        /// </summary>
        [Required]
        [StringLength(100)]
        public string GroupName { get; set; }

        /// <summary>
        /// 是否啟用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }

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
