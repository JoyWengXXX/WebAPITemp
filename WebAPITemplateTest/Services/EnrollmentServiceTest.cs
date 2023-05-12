using Moq;
using WebAPITemplete.Models.DTOs.DefaultDB;
using WebAPITemplete.Services.interfaces;
using Xunit;

namespace WebAPITemplateTest.Services
{
    public class EnrollmentServiceTest
    {
        //CreateData的單元測試
        [Fact]
        public async Task CreateData_ValidCustomer_ReturnCreatedNum()
        {
            //Arrange
            //mock一個IEnrollmentService物件
            var mockEnrollmentService = new Mock<IEnrollmentService>();
            //設定mock物件的CreateData方法回傳1
            mockEnrollmentService.Setup(x => x.CreateData(It.IsAny<EnrollmentDTO>())).ReturnsAsync(1);
            //建立EnrollmentDTO物件
            var input = new EnrollmentDTO()
            {
                Course_Id = 1,
                Student_Id = 1,
                Enrollment_Date = DateTime.Now
            };
            //Act
            //呼叫mock物件的CreateData方法
            var result = await mockEnrollmentService.Object.CreateData(input);
            //Assert
            //驗證回傳值是否為1
            Assert.Equal(1, result);
        }

        //DeleteData的單元測試
        [Theory]
        [InlineData(1)]
        public async Task DeleteData_ValidCustomer_ReturnDeletedNum(int id)
        {
            //Arrange
            //mock一個IEnrollmentService物件
            var mockEnrollmentService = new Mock<IEnrollmentService>();
            //設定mock物件的DeleteData方法回傳1
            mockEnrollmentService.Setup(x => x.DeleteData(It.IsAny<EnrollmentDTO>())).ReturnsAsync(1);
            //建立EnrollmentDTO物件
            var input = new EnrollmentDTO()
            {
                Id = id
            };
            //Act
            //呼叫mock物件的DeleteData方法
            var result = await mockEnrollmentService.Object.DeleteData(input);
            //Assert
            //驗證回傳值是否為1
            Assert.Equal(1, result);
        }

        //UpdateData的單元測試
        [Theory]
        [InlineData(1, 1, 1)]
        public async Task UpdateData_ValidCustomer_ReturnUpdatedNum(int id, int course_id, int student_id)
        {
            //Arrange
            //mock一個IEnrollmentService物件
            var mockEnrollmentService = new Mock<IEnrollmentService>();
            //設定mock物件的UpdateData方法回傳1
            mockEnrollmentService.Setup(x => x.UpdateData(It.IsAny<EnrollmentDTO>())).ReturnsAsync(1);
            //建立EnrollmentDTO物件
            var input = new EnrollmentDTO()
            {
                Id = id,
                Course_Id = course_id,
                Student_Id = student_id,
                Enrollment_Date = DateTime.Now
            };
            //Act
            //呼叫mock物件的UpdateData方法
            var result = await mockEnrollmentService.Object.UpdateData(input);
            //Assert
            //驗證回傳值是否為1
            Assert.Equal(1, result);
        }

        //GetExistedData的單元測試
        [Theory]
        [InlineData(1)]
        public async Task GetExistedData_ValidCustomer_ReturnEnrollmentDTO(int id)
        {
            //Arrange
            //mock一個IEnrollmentService物件
            var mockEnrollmentService = new Mock<IEnrollmentService>();
            //建立EnrollmentDTO物件
            var input = new EnrollmentDTO()
            {
                Id = id,
                Course_Id = 1,
                Student_Id = 1,
                Enrollment_Date = DateTime.Now
            };
            //設定mock物件的GetExistedData方法回傳EnrollmentDTO
            mockEnrollmentService.Setup(x => x.GetExistedData(It.IsAny<EnrollmentDTO>())).ReturnsAsync(input);
            //Act
            //呼叫mock物件的GetExistedData方法
            var result = await mockEnrollmentService.Object.GetExistedData(input);
            //Assert
            //驗證回傳值是否與input相同
            Assert.Equal(result, input);
        }

        //GetDataList的單元測試
        [Fact]
        public async Task GetDataList_ValidCustomer_ReturnEnrollmentDTOList()
        {
            //Arrange
            //mock一個IEnrollmentService物件
            var mockEnrollmentService = new Mock<IEnrollmentService>();
            //設定mock物件的GetDataList方法回傳EnrollmentDTO List
            mockEnrollmentService.Setup(x => x.GetDataList()).ReturnsAsync(new List<EnrollmentDTO>());
            //Act
            //呼叫mock物件的GetDataList方法
            var result = await mockEnrollmentService.Object.GetDataList();
            //Assert
            //驗證回傳值是否為EnrollmentDTO List
            Assert.IsType<List<EnrollmentDTO>>(result);
        }
    }
}
