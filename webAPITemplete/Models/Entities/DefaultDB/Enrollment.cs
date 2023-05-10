using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webAPITemplete.Models.Entities.DefaultDB
{
    public class Enrollment
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 學生ID
        /// </summary>
        [Required]
        [ForeignKey("Student_Id")]
        public Student Student { get; set; }
        /// <summary>
        /// 課程ID
        /// </summary>
        [Required]
        [ForeignKey("Course_Id")]
        public Course Course { get; set; }
        /// <summary>
        /// 註冊日期
        /// </summary>
        public DateTime Enrollment_Date { get; set; }
    }
}
