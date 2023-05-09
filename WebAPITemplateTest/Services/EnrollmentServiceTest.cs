using Moq;
using webAPITemplete.Models.DTOs;
using webAPITemplete.Repository.Dapper.interfaces;
using webAPITemplete.Services;
using Xunit;

namespace WebAPITemplateTest.Services
{
    public class EnrollmentServiceTest
    {
        //CreateData的單元測試
        [Fact]
        public async Task CreateDataTest()
        {
            //Arrange
            var mock = new Mock<IBaseDapper<Enrollment>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<Enrollment>())).ReturnsAsync(1);
            var service = new EnrollmentService(mock.Object);
            var input = new Enrollment()
            {
                Course_Id = 1,
                Student_Id = 1,
                Enrollment_Date = DateTime.Now,
            };
            //Act
            var result = await service.CreateData(input);
            //Assert
            Assert.True(result);
        }

        //DeleteData的單元測試
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task DeleteDataTest(int id)
        {
            //Arrange
            var mock = new Mock<IBaseDapper<Enrollment>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<Enrollment>())).ReturnsAsync(1);
            var service = new EnrollmentService(mock.Object);
            var input = new Enrollment()
            {
                Id = id
            };
            //Act
            var result = await service.DeleteData(input);
            //Assert
            Assert.True(result);
        }

        //UpdateData的單元測試
        [Fact]
        public async Task UpdateDataTest()
        {
            //Arrange
            var mock = new Mock<IBaseDapper<Enrollment>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<Enrollment>())).ReturnsAsync(1);
            var service = new EnrollmentService(mock.Object);
            var input = new Enrollment()
            {
                Id = 1,
                Course_Id = 1,
                Student_Id = 1,
                Enrollment_Date = DateTime.Now,
            };
            //Act
            var result = await service.UpdateData(input);
            //Assert
            Assert.True(result);
        }

        //GetDataList的單元測試
        [Fact]
        public async Task GetDataListTest()
        {
            //Arrange
            var mock = new Mock<IBaseDapper<Enrollment>>();
            mock.Setup(x => x.QueryListData(It.IsAny<string>())).ReturnsAsync(new List<Enrollment>());
            var service = new EnrollmentService(mock.Object);
            //Act
            var result = await service.GetDataList();
            //Assert
            Assert.NotNull(result);
        }

        //GetExistedData的單元測試
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetExistedDataTest(int id)
        {
            //Arrange
            var mock = new Mock<IBaseDapper<Enrollment>>();
            mock.Setup(x => x.QuerySingleData(It.IsAny<string>(), It.IsAny<Enrollment>())).ReturnsAsync(new Enrollment());
            var service = new EnrollmentService(mock.Object);
            var input = new Enrollment()
            {
                Id = id
            };
            //Act
            var result = await service.GetExistedData(input);
            //Assert
            Assert.NotNull(result);
        }
    }
}
