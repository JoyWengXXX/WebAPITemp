using System.Data;

namespace webAPITemplete.Repository.Dapper.DbContexts.interfaces
{
    public interface IProjectDBContext
    {
        IDbConnection CreateConnection();
    }
}