using System.Data;

namespace CommomLibrary.Dapper.Repository.interfaces
{
    public interface IProjectDBContext
    {
        IDbConnection CreateConnection();
    }
}