using System.Data;
using webAPITemplete.Repository.Dapper.DbContexts.interfaces;

namespace webAPITemplete.Repository.Dapper.interfaces
{
    /// <summary>
    /// Dapper建立連線
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseDapper<T> where T : IProjectDBContext
    {
        //回傳DbContext的資料庫連線
        IDbConnection CreateConnection();
    }
}
