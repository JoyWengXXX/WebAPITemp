using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRTemplete.Models.Entities.DefaultDB
{
    /// <summary>
    /// 使用者密碼紀錄
    /// </summary>
    public class UserPasswordRecord
    {
        /// <summary>
        /// 流水編號
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerialNum { get; set; }
        /// <summary>
        /// 使用者流水編號
        /// </summary>
        [Required]
        public int UserInfoSerialNum { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
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
    }
}
