using WebAPITemplete.Services.interfaces;
using System.Text;
using WebAPITemplete.Models.DTOs.DefaultDB;
using Dapper;
using System.Data;
using WebAPITemplete.DBContexts.Dapper;
using CommomLibrary.Dapper.Repository.interfaces;

namespace WebAPITemplete.Services
{
    public class CourseService : ICourseService
    {
        private readonly IDbConnection _dbConnection;

        public CourseService(IBaseDapper<ProjectDBContext_Default> _baseDapperDefault)
        {
            _dbConnection = _baseDapperDefault.CreateConnection();
        }

        public async Task<int> CreateData(CourseDTO input)
        {
            return await _dbConnection.ExecuteAsync(@"INSERT INTO Course (Name,Descript) VALUES (@Name,@Descript)", input);
        }

        public async Task<int> DeleteData(CourseDTO input)
        {
            return await _dbConnection.ExecuteAsync(@"DELETE FROM Course WHERE Id = @Id", input);
        }

        public async Task<int> UpdateData(CourseDTO input)
        {
            //使用StringBuilder組合Update的SQL
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Course SET ");
            if (!string.IsNullOrEmpty(input.Name))
            {
                sb.Append("Name = @Name, ");
            }
            if (!string.IsNullOrEmpty(input.Descript))
            {
                sb.Append("Descript = @Descript ");
            }
            sb.Append("WHERE Id = @Id");
            //執行SQL指令
            return await _dbConnection.ExecuteAsync(sb.ToString(), input);
        }

        public async Task<IEnumerable<CourseDTO>?> GetDataList()
        {
            //檢查資料表中是否有資料
            if (await _dbConnection.QuerySingleOrDefaultAsync<CourseDTO>(@"SELECT COUNT(*) FROM Course") == null)
                return null;
            else
                return await _dbConnection.QueryAsync<CourseDTO>(@"SELECT TOP(1000) * FROM Course");
        }

        public async Task<CourseDTO?> GetExistedData(CourseDTO input)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<CourseDTO>(@"SELECT COUNT(*) FROM Course WHERE Id = @Id", input);
        }
    }
}
