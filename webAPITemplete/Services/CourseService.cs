using webAPITemplete.Services.interfaces;
using System.Text;
using webAPITemplete.Repository.Dapper.interfaces;
using webAPITemplete.Repository.Dapper.DbContexts;
using webAPITemplete.Models.DTOs.DefaultDB;

namespace webAPITemplete.Services
{
    public class CourseService : ICourseService
    {
        private readonly IBaseDapper<CourseDTO, ProjectDBContext_Default> _baseDapperDefault;

        public CourseService(IBaseDapper<CourseDTO, ProjectDBContext_Default> baseDapperDefault) 
        {
            this._baseDapperDefault = baseDapperDefault;
        }

        public async Task<bool> CreateData(CourseDTO input)
        {
            if(await _baseDapperDefault.ExecuteCommand(@"INSERT INTO Course (Name,Descript) VALUES (@Name,@Descript)", input) > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteData(CourseDTO input)
        {
            if(await _baseDapperDefault.ExecuteCommand(@"DELETE FROM Course WHERE Id = @Id", input) > 0)
                return true;
            else
                return false;
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
            if(await _baseDapperDefault.ExecuteCommand(sb.ToString(), input) > 0)
                return true;
            else
                return false;
        }

        public async Task<List<CourseDTO>?> GetDataList()
        {
            return await _baseDapperDefault.QueryListData(@"SELECT TOP(1000) * FROM Course");
        }

        public async Task<CourseDTO?> GetExistedData(CourseDTO input)
        {
            return await _baseDapperDefault.QuerySingleData(@"SELECT * FROM Course WHERE Id = @Id", input);
        }
    }
}
