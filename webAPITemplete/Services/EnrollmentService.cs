using webAPITemplete.Services.interfaces;
using webAPITemplete.Models.DTOs;
using System.Text;
using webAPITemplete.Repository.Dapper.interfaces;

namespace webAPITemplete.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IBaseDapper<Enrollment> _baseDapper;

        public EnrollmentService(IBaseDapper<Enrollment> baseDapper) 
        {
            this._baseDapper = baseDapper;
        }

        public async Task<bool> CreateData(Enrollment input)
        {
            if (await _baseDapper.ExecuteCommand(@"INSERT INTO Enrollment (Student_Id,Course_Id,Enrollment_Date) VALUES (@Student_Id,@Course_Id,@Enrollment_Date)", input) > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteData(Enrollment input)
        {
            if(await _baseDapper.ExecuteCommand(@"DELETE FROM Enrollment WHERE Id = @Id", input) > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateData(Enrollment input)
        {
            //使用StringBuilder組合Update的SQL
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Enrollment SET ");
            sb.Append("Student_Id = @Student_Id, ");
            sb.Append("Course_Id = @Course_Id, ");
            sb.Append("Enrollment_Date = @Enrollment_Date ");
            sb.Append("WHERE Id = @Id");
            //執行SQL
            if(await _baseDapper.ExecuteCommand(sb.ToString(), input) > 0)
                return true;
            else
                return false;
        }

        public async Task<List<Enrollment>?> GetDataList()
        {
            return await _baseDapper.QueryListData(@"SELECT TOP(1000) * FROM Enrollment");
        }

        public async Task<Enrollment?> GetExistedData(Enrollment input)
        {
            return await _baseDapper.QuerySingleData(@"SELECT * FROM Enrollment WHERE Id = @Id", input);
        }
    }
}
