using webAPITemplete.Repository.Dapper.interfaces;
using Dapper;
using webAPITemplete.Repository.Dapper.DbContexts;

namespace webAPITemplete.Repository.Dapper.services
{
    public class BaseDapper<T> : IBaseDapper<T> where T : class
    {
        private readonly ProjectDBContext_1 _dBContext;
        public BaseDapper(ProjectDBContext_1 DBContext) { this._dBContext = DBContext; }

        public async Task<int> ExecuteCommand(string sql, T parameters)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }
        public async Task<List<T>?> QueryListData(string sql)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                //QueryMultipleAsync，並回傳List；如果Query查不到資料則回傳Null
                return (await connection.QueryMultipleAsync(sql)).Read<T>().Any() ? (await connection.QueryMultipleAsync(sql)).Read<T>().ToList() : null;
            }
        }
        public async Task<T?> QuerySingleData(string sql, T parameters)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
                return result;
            }
        }
    }
}
