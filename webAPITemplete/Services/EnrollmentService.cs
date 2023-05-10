using webAPITemplete.Services.interfaces;
using System.Text;
using webAPITemplete.Repository.Dapper.interfaces;
using webAPITemplete.Repository.Dapper.DbContexts;
using webAPITemplete.Models.DTOs.DefaultDB;
using Dapper;

namespace webAPITemplete.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IBaseDapper<ProjectDBContext_Default> _baseDapperDefault;

        public EnrollmentService(IBaseDapper<ProjectDBContext_Default> baseDapperDefault) 
        {
            _baseDapperDefault = baseDapperDefault;
        }

        public async Task<bool> CreateData(EnrollmentDTO input)
        {
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                if (await connection.ExecuteAsync(@"INSERT INTO Enrollment (Student_Id,Course_Id,Enrollment_Date) VALUES (@Student_Id,@Course_Id,@Enrollment_Date)", input) > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> DeleteData(EnrollmentDTO input)
        {
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                if (await connection.ExecuteAsync(@"DELETE FROM Enrollment WHERE Id = @Id", input) > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> UpdateData(EnrollmentDTO input)
        {
            //使用StringBuilder組合Update的SQL
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Enrollment SET ");
            sb.Append("Student_Id = @Student_Id, ");
            sb.Append("Course_Id = @Course_Id, ");
            sb.Append("Enrollment_Date = @Enrollment_Date ");
            sb.Append("WHERE Id = @Id");
            //執行SQL
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                if (await connection.ExecuteAsync(sb.ToString(), input) > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<IEnumerable<EnrollmentDTO>?> GetDataList()
        {
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                //檢查資料表中是否有資料
                if ((await connection.QueryAsync<EnrollmentDTO>(@"SELECT TOP(1) * FROM Enrollment")).Count() == 0)
                    return null;
                else
                    return (await connection.QueryAsync<EnrollmentDTO>(@"SELECT TOP(1000) * FROM Enrollment")).ToList();
            }
        }

        public async Task<EnrollmentDTO?> GetExistedData(EnrollmentDTO input)
        {
            using (var connection = _baseDapperDefault.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<EnrollmentDTO>(@"SELECT * FROM Enrollment WHERE Id = @Id", input);
            }
        }
    }
}
