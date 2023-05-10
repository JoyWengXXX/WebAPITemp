using webAPITemplete.Services.interfaces;
using System.Text;
using webAPITemplete.Repository.Dapper.interfaces;
using webAPITemplete.Repository.Dapper.DbContexts;
using webAPITemplete.Models.DTOs.DefaultDB;
using Dapper;

namespace webAPITemplete.Services
{
    public class StudentService : IStudentService
    {
        private readonly IBaseDapper<ProjectDBContext_Default> _baseDapperDefault;

        public StudentService(IBaseDapper<ProjectDBContext_Default> baseDapperDefault) 
        {
            _baseDapperDefault = baseDapperDefault;
        }

        public async Task<bool> CreateData(StudentDTO input)
        {
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                if (await connection.ExecuteAsync(@"INSERT INTO Student (Name,Email,Phone,Address) VALUES (@Name,@Email,@Phone,@Address)", input) > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> DeleteData(StudentDTO input)
        {
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                if (await connection.ExecuteAsync(@"DELETE FROM Student WHERE Id = @Id", input) > 0)
                    return true;
                else
                    return false;
            }
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
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                if (await connection.ExecuteAsync(sb.ToString(), input) > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<IEnumerable<StudentDTO>?> GetDataList()
        {
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                //檢查資料表中是否有資料
                if ((await connection.QueryAsync<EnrollmentDTO>(@"SELECT TOP(1) * FROM Student")).Count() == 0)
                    return null;
                else
                    return await connection.QueryAsync<StudentDTO>(@"SELECT TOP(1000) * FROM Student");
            }
        }

        public async Task<StudentDTO?> GetExistedData(StudentDTO input)
        {
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<StudentDTO>(@"SELECT * FROM Student WHERE Id = @Id", input);
            }
        }
    }
}
