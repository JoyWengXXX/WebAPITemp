using Moq;
using webAPITemplete.Models.DTOs.DefaultDB;
using webAPITemplete.Repository.Dapper.DbContexts;
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
            var mock = new Mock<IBaseDapper<EnrollmentDTO, ProjectDBContext_Default>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<EnrollmentDTO>())).ReturnsAsync(1);
            var service = new EnrollmentService(mock.Object);
            var input = new EnrollmentDTO()
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
            var mock = new Mock<IBaseDapper<EnrollmentDTO, ProjectDBContext_Default>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<EnrollmentDTO>())).ReturnsAsync(1);
            var service = new EnrollmentService(mock.Object);
            var input = new EnrollmentDTO()
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
            var mock = new Mock<IBaseDapper<EnrollmentDTO, ProjectDBContext_Default>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<EnrollmentDTO>())).ReturnsAsync(1);
            var service = new EnrollmentService(mock.Object);
            var input = new EnrollmentDTO()
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
            var mock = new Mock<IBaseDapper<EnrollmentDTO, ProjectDBContext_Default>>();
            mock.Setup(x => x.QueryListData(It.IsAny<string>())).ReturnsAsync(new List<EnrollmentDTO>());
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
            var mock = new Mock<IBaseDapper<EnrollmentDTO, ProjectDBContext_Default>>();
            mock.Setup(x => x.QuerySingleData(It.IsAny<string>(), It.IsAny<EnrollmentDTO>())).ReturnsAsync(new EnrollmentDTO());
            var service = new EnrollmentService(mock.Object);
            var input = new EnrollmentDTO()
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
