using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webAPITemplete.Models.Entities
{
    /// <summary>
    /// 課程
    /// </summary>
    public class Course
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(1000)]
        public string? Descript { get; set; }
    }
}
