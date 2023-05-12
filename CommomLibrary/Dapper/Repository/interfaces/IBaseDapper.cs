using System.Data;

namespace CommomLibrary.Dapper.Repository.interfaces
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
