﻿

namespace webAPITemplete.Services.interfaces
{
    /// <summary>
    /// 基礎功能介面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseServices<T> where T : class
    {
        /// <summary>
        /// 取回資料清單
        /// </summary>
        /// <returns></returns>
        public Task<List<T>?> GetDataList();
        /// <summary>
        /// 取回指定ID資料
        /// </summary>
        /// <returns></returns>
        public Task<T?> GetExistedData(T input);
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="input"></param>
        public Task<bool> CreateData(T input);
        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="input"></param>
        public Task<bool> UpdateData(T input);
        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="input"></param>
        public Task<bool> DeleteData(T input);
    }
}
