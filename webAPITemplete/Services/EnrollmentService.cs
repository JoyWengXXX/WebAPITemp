using webAPITemplete.Services.interfaces;
using System.Text;
using webAPITemplete.Repository.Dapper.interfaces;
using webAPITemplete.Repository.Dapper.DbContexts;
using webAPITemplete.Models.DTOs.DefaultDB;

namespace webAPITemplete.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IBaseDapper<EnrollmentDTO, ProjectDBContext_Default> _baseDapperDefault;

        public EnrollmentService(IBaseDapper<EnrollmentDTO, ProjectDBContext_Default> baseDapperDefault) 
        {
            _baseDapperDefault = baseDapperDefault;
        }

        public async Task<bool> CreateData(EnrollmentDTO input)
        {
            if (await _baseDapperDefault.ExecuteCommand(@"INSERT INTO Enrollment (Student_Id,Course_Id,Enrollment_Date) VALUES (@Student_Id,@Course_Id,@Enrollment_Date)", input) > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteData(EnrollmentDTO input)
        {
            if(await _baseDapperDefault.ExecuteCommand(@"DELETE FROM Enrollment WHERE Id = @Id", input) > 0)
                return true;
            else
                return false;
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
            if(await _baseDapperDefault.ExecuteCommand(sb.ToString(), input) > 0)
                return true;
            else
                return false;
        }

        public async Task<List<EnrollmentDTO>?> GetDataList()
        {
            return await _baseDapperDefault.QueryListData(@"SELECT TOP(1000) * FROM Enrollment");
        }

        public async Task<EnrollmentDTO?> GetExistedData(EnrollmentDTO input)
        {
            return await _baseDapperDefault.QuerySingleData(@"SELECT * FROM Enrollment WHERE Id = @Id", input);
        }
    }
}
