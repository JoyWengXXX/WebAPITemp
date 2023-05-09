using System.ComponentModel.DataAnnotations;

namespace webAPITemplete.Models.DTOs
{
    public class Enrollment
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Student_Id { get; set; }
        [Required]
        public int Course_Id { get; set; }
        [Required]
        public DateTime Enrollment_Date { get; set; }
    }
}