using webAPITemplete.Repository.Dapper.interfaces;
using Dapper;
using webAPITemplete.Repository.Dapper.DbContexts.interfaces;
using System.Data;

namespace webAPITemplete.Repository.Dapper.services
{
    public class BaseDapper<T> : IBaseDapper<T> where T : IProjectDBContext
    {
        private readonly IProjectDBContext _dbContext;

        public BaseDapper(T dbContext)
        {
            _dbContext = dbContext;
        }

        public IDbConnection CreateConnection()
        {
            return _dbContext.CreateConnection();
        }
    }
}
