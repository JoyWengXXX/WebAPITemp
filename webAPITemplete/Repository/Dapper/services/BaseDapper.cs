using webAPITemplete.Repository.Dapper.interfaces;
using Dapper;
using webAPITemplete.Repository.Dapper.DbContexts.interfaces;

namespace webAPITemplete.Repository.Dapper.services
{
    public class BaseDapper<T1, T2> : IBaseDapper<T1, T2> where T1 : class where T2 : IProjectDBContext
    {
        private readonly T2 _dBContext;
        public BaseDapper(T2 DBContext)
        {
            _dBContext = DBContext;
        }

        public async Task<int> ExecuteCommand(string sql, T1 parameters)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }
        public async Task<List<T1>?> QueryListData(string sql)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                //QueryMultipleAsync，並回傳List；如果Query查不到資料則回傳Null
                return (await connection.QueryMultipleAsync(sql)).Read<T1>().Any() ? (await connection.QueryMultipleAsync(sql)).Read<T1>().ToList() : null;
            }
        }
        public async Task<T1?> QuerySingleData(string sql, T1 parameters)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<T1>(sql, parameters);
                return result;
            }
        }
    }
}
