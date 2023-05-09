

namespace webAPITemplete.Repository.Dapper.interfaces
{
    /// <summary>
    /// Dapper基礎操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseDapper<T> where T : class
    {
        /// <summary>
        /// 查詢單筆資料
        /// </summary>
        /// <returns></returns>
        public Task<T?> QuerySingleData(string sql, T parameters);
        /// <summary>
        /// 查詢清單資料
        /// </summary>
        /// <returns></returns>
        public Task<List<T>?> QueryListData(string sql);
        /// <summary>
        /// 增刪改/其他SQL
        /// </summary>
        /// <returns></returns>
        public Task<int> ExecuteCommand(string sql, T parameters);
    }
}
