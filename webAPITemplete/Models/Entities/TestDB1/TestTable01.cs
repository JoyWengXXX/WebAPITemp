using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webAPITemplete.Models.Entities.TestDB1
{
    /// <summary>
    /// 範例DB Table 01
    /// </summary>
    public class TestTable01
    {
        /// <summary>
        ///  ID_1
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_1 { get; set; }
        /// <summary>
        ///  ID_2
        /// </summary>
        [Key]
        [Required]
        public int ID_2 { get; set; }
        /// <summary>
        ///  TestCol01
        /// </summary>
        [StringLength(25)]
        public string? TestCol01 { get; set; }
        /// <summary>
        ///  TestCol02
        /// </summary>
        public int TestCol02 { get; set; }
        /// <summary>
        ///  TestCol03
        /// </summary>
        [Required]
        public int ProductID { get; set; }
    }
}
