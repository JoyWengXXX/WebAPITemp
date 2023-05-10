using webAPITemplete.Repository.Dapper.DbContexts.interfaces;

namespace webAPITemplete.Repository.Dapper.interfaces
{
    /// <summary>
    /// Dapper基礎操作
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public interface IBaseDapper<T1, T2> where T1 : class where T2 : IProjectDBContext
    {
        /// <summary>
        /// 查詢單筆資料
        /// </summary>
        /// <returns></returns>
        public Task<T1?> QuerySingleData(string sql, T1 parameters);
        /// <summary>
        /// 查詢清單資料
        /// </summary>
        /// <returns></returns>
        public Task<List<T1>?> QueryListData(string sql);
        /// <summary>
        /// 增刪改/其他SQL
        /// </summary>
        /// <returns></returns>
        public Task<int> ExecuteCommand(string sql, T1 parameters);
    }
}
