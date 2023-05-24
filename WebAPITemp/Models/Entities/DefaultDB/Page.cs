using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webAPITemp.Models.Entities.DefaultDB
{
    public class Page
    {
        /// <summary>
        /// 功能頁編號
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PageID { get; set; }
        /// <summary>
        /// 所屬系統編號
        /// </summary>
        [Key]
        [Required]
        public int SystemID { get; set; }
        /// <summary>
        /// 功能頁名稱
        /// </summary>
        [Required]
        [StringLength(30)]
        public string PageName { get; set; }
        /// <summary>
        /// 上層功能頁編號
        /// </summary>
        [Required]
        public int ParentPageID { get; set; }
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
