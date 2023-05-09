using webAPITemplete.Services.interfaces;
using webAPITemplete.Models.DTOs;
using System.Text;
using webAPITemplete.Repository.Dapper.interfaces;

namespace webAPITemplete.Services
{
    public class CourseService : ICourseService
    {
        private readonly IBaseDapper<Course> _baseDapper;

        public CourseService(IBaseDapper<Course> baseDapper) 
        {
            this._baseDapper = baseDapper;
        }

        public async Task<bool> CreateData(Course input)
        {
            if(await _baseDapper.ExecuteCommand(@"INSERT INTO Course (Name,Descript) VALUES (@Name,@Descript)", input) > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteData(Course input)
        {
            if(await _baseDapper.ExecuteCommand(@"DELETE FROM Course WHERE Id = @Id", input) > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateData(Course input)
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
            if(await _baseDapper.ExecuteCommand(sb.ToString(), input) > 0)
                return true;
            else
                return false;
        }

        public async Task<List<Course>?> GetDataList()
        {
            return await _baseDapper.QueryListData(@"SELECT TOP(1000) * FROM Course");
        }

        public async Task<Course?> GetExistedData(Course input)
        {
            return await _baseDapper.QuerySingleData(@"SELECT * FROM Course WHERE Id = @Id", input);
        }
    }
}
