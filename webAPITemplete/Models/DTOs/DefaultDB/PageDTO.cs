namespace WebAPITemplete.Models.DTOs.DefaultDB
{
    public class PageDTO
    {
        /// <summary>
        /// 頁面標號
        /// </summary>
        public int PageID { get; set; }
        /// <summary>
        /// 頁面名稱
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// 上層功能頁編號
        /// </summary>
        public int ParentPageID { get; set; }
        /// <summary>
        /// 頁面層數
        /// </summary>
        public int Level { get; set; }
    }
}
