using Moq;
using System.Net;
using System.Numerics;
using WebAPITemplete.Models.DTOs.DefaultDB;
using WebAPITemplete.Services.interfaces;
using Xunit;

namespace WebAPITemplateTest.Services
{
    public class StudentServiceTest
    {
        //CreateData的單元測試
        [Fact]
        public async Task CreateData_ValidCustomer_ReturnCreatedNum()
        {
            //Arrange
            //mock一個IStudentService物件
            var mockStudentService = new Mock<IStudentService>();
            //設定mock物件的CreateData方法回傳1
            mockStudentService.Setup(x => x.CreateData(It.IsAny<StudentDTO>())).ReturnsAsync(1);
            //建立StudentDTO物件
            var input = new StudentDTO()
            {
                Name = "Test",
                Address = "Test",
                Phone = "Test",
                Email = "Test",
            };
            //Act
            //呼叫mock物件的CreateData方法
            var result = await mockStudentService.Object.CreateData(input);
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
            //mock一個IStudentService物件
            var mockStudentService = new Mock<IStudentService>();
            //設定mock物件的DeleteData方法回傳1
            mockStudentService.Setup(x => x.DeleteData(It.IsAny<StudentDTO>())).ReturnsAsync(1);
            //建立StudentDTO物件
            var input = new StudentDTO()
            {
                Id = id
            };
            //Act
            //呼叫mock物件的DeleteData方法
            var result = await mockStudentService.Object.DeleteData(input);
            //Assert
            //驗證回傳值是否為1
            Assert.Equal(1, result);
        }

        //UpdateData的單元測試
        [Theory]
        [InlineData(1, "Test", "Test", "Test", "Test")]
        public async Task UpdateData_ValidCustomer_ReturnUpdatedNum(int id, string name, string address, string phone, string email)
        {
            //Arrange
            //mock一個IStudentService物件
            var mockStudentService = new Mock<IStudentService>();
            //設定mock物件的UpdateData方法回傳1
            mockStudentService.Setup(x => x.UpdateData(It.IsAny<StudentDTO>())).ReturnsAsync(1);
            //建立StudentDTO物件
            var input = new StudentDTO()
            {
                Id = id,
                Name = name,
                Address = address,
                Phone = phone,
                Email = email,
            };
            //Act
            //呼叫mock物件的UpdateData方法
            var result = await mockStudentService.Object.UpdateData(input);
            //Assert
            //驗證回傳值是否為1
            Assert.Equal(1, result);
        }

        //GetExistedData的單元測試
        [Theory]
        [InlineData(1)]
        public async Task GetExistedData_ValidCustomer_ReturnStudentDTO(int id)
        {
            //Arrange
            //mock一個IStudentService物件
            var mockStudentService = new Mock<IStudentService>();
            //建立StudentDTO物件
            var input = new StudentDTO()
            {
                Id = id,
                Name = "Test",
                Address = "Test",
                Phone = "Test",
                Email = "Test",
            };
            //設定mock物件的GetExistedData方法回傳StudentDTO
            mockStudentService.Setup(x => x.GetExistedData(It.IsAny<StudentDTO>())).ReturnsAsync(input);
            //Act
            //呼叫mock物件的GetExistedData方法
            var result = await mockStudentService.Object.GetExistedData(input);
            //Assert
            //驗證回傳值是否與input相同
            Assert.Equal(result, input);
        }

        //GetDataList的單元測試
        [Fact]
        public async Task GetDataList_ValidCustomer_ReturnStudentDTOList()
        {
            //Arrange
            //mock一個IStudentService物件
            var mockEnrollmentService = new Mock<IStudentService>();
            //設定mock物件的GetDataList方法回傳StudentDTO List
            mockEnrollmentService.Setup(x => x.GetDataList()).ReturnsAsync(new List<StudentDTO>());
            //Act
            //呼叫mock物件的GetDataList方法
            var result = await mockEnrollmentService.Object.GetDataList();
            //Assert
            //驗證回傳值是否為EnrollmentDTO List
            Assert.IsType<List<StudentDTO>>(result);
        }
    }
}
