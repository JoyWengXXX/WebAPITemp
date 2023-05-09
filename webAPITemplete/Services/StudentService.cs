using webAPITemplete.Services.interfaces;
using webAPITemplete.Models.DTOs;
using System.Text;
using webAPITemplete.Repository.Dapper.interfaces;

namespace webAPITemplete.Services
{
    public class StudentService : IStudentService
    {
        private readonly IBaseDapper<Student> _baseDapper;

        public StudentService(IBaseDapper<Student> baseDapper) 
        {
            this._baseDapper = baseDapper;
        }

        public async Task<bool> CreateData(Student input)
        {
            if (await _baseDapper.ExecuteCommand(@"INSERT INTO Student (Name,Email,Phone,Address) VALUES (@Name,@Email,@Phone,@Address)", input) > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteData(Student input)
        {
            if(await _baseDapper.ExecuteCommand(@"DELETE FROM Student WHERE Id = @Id", input) > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateData(Student input)
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
            if(await _baseDapper.ExecuteCommand(sb.ToString(), input) > 0)
                return true;
            else
                return false;
        }

        public async Task<List<Student>?> GetDataList()
        {
            return await _baseDapper.QueryListData(@"SELECT TOP(1000) * FROM Student");
        }

        public async Task<Student?> GetExistedData(Student input)
        {
            return await _baseDapper.QuerySingleData(@"SELECT * FROM Student WHERE Id = @Id", input);
        }
    }
}
