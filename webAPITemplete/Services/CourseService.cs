using webAPITemplete.Services.interfaces;
using System.Text;
using webAPITemplete.Repository.Dapper.interfaces;
using webAPITemplete.Repository.Dapper.DbContexts;
using webAPITemplete.Models.DTOs.DefaultDB;
using webAPITemplete.Models.DTOs.TestDB1;
using Dapper;

namespace webAPITemplete.Services
{
    public class CourseService : ICourseService
    {
        private readonly IBaseDapper<ProjectDBContext_Default> _baseDapperDefault;
        //private readonly IBaseDapper<ProjectDBContext_Test1> _baseDapperTest; 

        public CourseService(
                                IBaseDapper<ProjectDBContext_Default> _baseDapperDefault
                                //,IBaseDapper<ProjectDBContext_Test1> _baseDapperTest
                            )
        {
            this._baseDapperDefault = _baseDapperDefault;
            //this._baseDapperTest = _baseDapperTest;
        }

        public async Task<bool> CreateData(CourseDTO input)
        {
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                if (await connection.ExecuteAsync(@"INSERT INTO Course (Name,Descript) VALUES (@Name,@Descript)", input) > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> DeleteData(CourseDTO input)
        {
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                if (await connection.ExecuteAsync(@"DELETE FROM Course WHERE Id = @Id", input) > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> UpdateData(CourseDTO input)
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
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                if (await connection.ExecuteAsync(sb.ToString(), input) > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<IEnumerable<CourseDTO>?> GetDataList()
        {
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                //檢查資料表中是否有資料
                if (await connection.QuerySingleOrDefaultAsync<CourseDTO>(@"SELECT COUNT(*) FROM Course") == null)
                    return null;
                else
                    return await connection.QueryAsync<CourseDTO>(@"SELECT TOP(1000) * FROM Course");
            }
        }

        public async Task<CourseDTO?> GetExistedData(CourseDTO input)
        {
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<CourseDTO>(@"SELECT COUNT(*) FROM Course WHERE Id = @Id", input);
            }
        }
    }
}
