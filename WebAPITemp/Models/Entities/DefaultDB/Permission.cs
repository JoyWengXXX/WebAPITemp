using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webAPITemp.Models.Entities.DefaultDB
{
    public class Permission
    {
        /// <summary>
        /// 角色權限編號
        /// </summary>
        [Key]
        [Required]
        public int RoleID { get; set; }
        /// <summary>
        /// 功能頁編號
        /// </summary>
        [Key]
        [Required]
        public int PageID { get; set; }
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
