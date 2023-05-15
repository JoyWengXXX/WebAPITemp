using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPITemplete.Models.Entities.DefaultDB
{
    /// <summary>
    /// 使用者資料
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 使用者編號
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerialNum { get; set; }

        /// <summary>
        /// 使用者編號
        /// </summary>
        [Key]
        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        /// <summary>
        /// 腳色權限編號
        /// </summary>
        [Required]
        public int RoleID { get; set; }

        /// <summary>
        /// 使用者姓
        /// </summary>
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        /// <summary>
        /// 使用者名
        /// </summary>
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        /// <summary>
        /// 使用者性別
        /// </summary>
        [StringLength(1)]
        public Gender? Gender { get; set; }

        /// <summary>
        /// 使用者生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 使用者電話
        /// </summary>
        [StringLength(12)]
        public string? Phone { get; set; }

        /// <summary>
        /// 使用者信箱
        /// </summary>
        [StringLength(254)]
        public string? Email { get; set; }

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

    public enum Gender
    {
        Male = 'M',
        Femail = 'F'
    }
}
