using webAPITemplete.Services.interfaces;
using System.Text;
using webAPITemplete.Repository.Dapper.interfaces;
using webAPITemplete.Repository.Dapper.DbContexts;
using webAPITemplete.Models.DTOs.DefaultDB;

namespace webAPITemplete.Services
{
    public class StudentService : IStudentService
    {
        private readonly IBaseDapper<StudentDTO, ProjectDBContext_Default> _baseDapperDefault;

        public StudentService(IBaseDapper<StudentDTO, ProjectDBContext_Default> baseDapperDefault) 
        {
            _baseDapperDefault = baseDapperDefault;
        }

        public async Task<bool> CreateData(StudentDTO input)
        {
            if (await _baseDapperDefault.ExecuteCommand(@"INSERT INTO Student (Name,Email,Phone,Address) VALUES (@Name,@Email,@Phone,@Address)", input) > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteData(StudentDTO input)
        {
            if(await _baseDapperDefault.ExecuteCommand(@"DELETE FROM Student WHERE Id = @Id", input) > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateData(StudentDTO input)
        {
            //使用StringBuilder組合Update的SQL
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Student SET");
            if (!string.IsNullOrEmpty(input.Name))
            {
                sb.Append(" Name = @Name,");
            }
            if (!string.IsNullOrEmpty(input.Email))
            {
                sb.Append(" Email = @Email,");
            }
            if (!string.IsNullOrEmpty(input.Phone))
            {
                sb.Append(" Phone = @Phone,");
            }
            if (!string.IsNullOrEmpty(input.Address))
            {
                sb.Append(" Address = @Address");
            }
            sb.Append(" WHERE Id = @Id");
            //執行Query
            if(await _baseDapperDefault.ExecuteCommand(sb.ToString(), input) > 0)
                return true;
            else
                return false;
        }

        public async Task<List<StudentDTO>?> GetDataList()
        {
            return await _baseDapperDefault.QueryListData(@"SELECT TOP(1000) * FROM Student");
        }

        public async Task<StudentDTO?> GetExistedData(StudentDTO input)
        {
            return await _baseDapperDefault.QuerySingleData(@"SELECT * FROM Student WHERE Id = @Id", input);
        }
    }
}
