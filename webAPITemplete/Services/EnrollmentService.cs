using WebAPITemplete.Services.interfaces;
using System.Text;
using WebAPITemplete.Models.DTOs.DefaultDB;
using Dapper;
using System.Data;
using WebAPITemplete.DBContexts.Dapper;
using CommomLibrary.Dapper.Repository.interfaces;

namespace WebAPITemplete.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IDbConnection _dbConnection;

        public EnrollmentService(IBaseDapper<ProjectDBContext_Default> baseDapperDefault) 
        {
            _dbConnection = baseDapperDefault.CreateConnection();
        }

        public async Task<int> CreateData(EnrollmentDTO input)
        {
            return await _dbConnection.ExecuteAsync(@"INSERT INTO Enrollment (Student_Id,Course_Id,Enrollment_Date) VALUES (@Student_Id,@Course_Id,@Enrollment_Date)", input);
        }

        public async Task<int> DeleteData(EnrollmentDTO input)
        {
            return await _dbConnection.ExecuteAsync(@"DELETE FROM Enrollment WHERE Id = @Id", input);
        }

        public async Task<int> UpdateData(EnrollmentDTO input)
        {
            //使用StringBuilder組合Update的SQL
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Enrollment SET ");
            sb.Append("Student_Id = @Student_Id, ");
            sb.Append("Course_Id = @Course_Id, ");
            sb.Append("Enrollment_Date = @Enrollment_Date ");
            sb.Append("WHERE Id = @Id");
            //執行SQL
            return await _dbConnection.ExecuteAsync(sb.ToString(), input);
        }

        public async Task<IEnumerable<EnrollmentDTO>?> GetDataList()
        {
            //檢查資料表中是否有資料
            if ((await _dbConnection.QueryAsync<EnrollmentDTO>(@"SELECT TOP(1) * FROM Enrollment")).Count() == 0)
                return null;
            else
                return (await _dbConnection.QueryAsync<EnrollmentDTO>(@"SELECT TOP(1000) * FROM Enrollment")).ToList();
        }

        public async Task<EnrollmentDTO?> GetExistedData(EnrollmentDTO input)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<EnrollmentDTO>(@"SELECT * FROM Enrollment WHERE Id = @Id", input);
        }
    }
}
