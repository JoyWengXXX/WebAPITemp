using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webAPITemplete.Models.Entities.DefaultDB
{
    public class Student
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        /// <summary>
        /// 電子郵件
        /// </summary>
        [StringLength(500)]
        public string? Email { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        [StringLength(12)]
        public string? Phone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string? Address { get; set; }
    }
}