using webAPITemplete.Services.interfaces;
using System.Text;
using webAPITemplete.Repository.Dapper.interfaces;
using webAPITemplete.Repository.Dapper.DbContexts;
using webAPITemplete.Models.DTOs.DefaultDB;
using Dapper;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace webAPITemplete.Services
{
    public class StudentService : IStudentService
    {
        private readonly IDbConnection _dbConnection;

        public StudentService(IBaseDapper<ProjectDBContext_Default> baseDapperDefault) 
        {
            _dbConnection = baseDapperDefault.CreateConnection();
        }

        public async Task<int> CreateData(StudentDTO input)
        {
            return await _dbConnection.ExecuteAsync(@"INSERT INTO Student (Name,Email,Phone,Address) VALUES (@Name,@Email,@Phone,@Address)", input);
        }

        public async Task<int> DeleteData(StudentDTO input)
        {
            return await _dbConnection.ExecuteAsync(@"DELETE FROM Student WHERE Id = @Id", input);
        }

        public async Task<int> UpdateData(StudentDTO input)
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
            return await _dbConnection.ExecuteAsync(sb.ToString(), input);
        }

        public async Task<IEnumerable<StudentDTO>?> GetDataList()
        {
            //檢查資料表中是否有資料
            if ((await _dbConnection.QueryAsync<EnrollmentDTO>(@"SELECT TOP(1) * FROM Student")).Count() == 0)
                return null;
            else
                return await _dbConnection.QueryAsync<StudentDTO>(@"SELECT TOP(1000) * FROM Student");
        }

        public async Task<StudentDTO?> GetExistedData(StudentDTO input)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<StudentDTO>(@"SELECT * FROM Student WHERE Id = @Id", input);
        }
    }
}
