using CommomLibrary.Dapper.Repository.interfaces;
using System.Data;

namespace CommomLibrary.Dapper.Repository.services
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
