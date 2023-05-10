using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webAPITemplete.Models.Entities.TestDB1
{
    /// <summary>
    /// 參考SQL Server範例資料庫AdventureWorks2019
    /// </summary>
    public class SalesOrderDetail
    {
        /// <summary>
        ///  訂單明細ID
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesOrderID { get; set; }
        /// <summary>
        ///  訂單ID
        /// </summary>
        [Key]
        [Required]
        public int SalesOrderDetailID { get; set; }
        /// <summary>
        ///  訂單數量
        /// </summary>
        [StringLength(25)]
        public int OrderQty { get; set; }
        /// <summary>
        ///  產品ID
        /// </summary>
        [Required]
        public int ProductID { get; set; }
        /// <summary>
        ///  特價ID
        /// </summary>
        [Required]
        public int SpecialOfferID { get; set; }
        /// <summary>
        ///  單價
        /// </summary>
        [Required]
        public decimal UnitPrice { get; set; }
        /// <summary>
        ///  單價折扣
        /// </summary>
        [Required]
        public decimal UnitPriceDiscount { get; set; }
        /// <summary>
        ///  行總計
        /// </summary>
        [Required]
        public decimal LineTotal { get; set; }
        /// <summary>
        ///  產品標準成本
        /// </summary>
        [Required]
        public Guid rowguid { get; set; }
        /// <summary>
        ///  修改日期
        /// </summary>
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
