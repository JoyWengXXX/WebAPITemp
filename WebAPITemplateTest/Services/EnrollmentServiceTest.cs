using Microsoft.Extensions.Configuration;
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
        //取得相依專案webAPITemplete的configuration
        public IConfigurationRoot Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        //CreateData的單元測試
        [Fact]
        public async Task CreateDataTest()
        {
            //Arrange
            var mock = new Mock<IBaseDapper<ProjectDBContext_Default>>();
            mock.Setup(x => x.CreateConnection()).Returns(new ProjectDBContext_Default(Configuration.GetConnectionString("DefaultConnection")).CreateConnection());
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
        public async Task DeleteDataTest(int id)
        {
            //Arrange
            var mock = new Mock<IBaseDapper<ProjectDBContext_Default>>();
            mock.Setup(x => x.CreateConnection()).Returns(new ProjectDBContext_Default(Configuration.GetConnectionString("DefaultConnection")).CreateConnection());
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
        [Theory]
        [InlineData(1)]
        public async Task UpdateDataTest(int id)
        {
            //Arrange
            var mock = new Mock<IBaseDapper<ProjectDBContext_Default>>();
            mock.Setup(x => x.CreateConnection()).Returns(new ProjectDBContext_Default(Configuration.GetConnectionString("DefaultConnection")).CreateConnection());
            var service = new EnrollmentService(mock.Object);
            var input = new EnrollmentDTO()
            {
                Id = id,
                Course_Id = 1,
                Student_Id = 1,
                Enrollment_Date = DateTime.Now,
            };
            //Act
            var result = await service.UpdateData(input);
            //Assert
            Assert.True(result);
        }

        //GetExistedData的單元測試
        [Theory]
        [InlineData(1)]
        public async Task GetDataTest(int id)
        {
            //Arrange
            var mock = new Mock<IBaseDapper<ProjectDBContext_Default>>();
            mock.Setup(x => x.CreateConnection()).Returns(new ProjectDBContext_Default(Configuration.GetConnectionString("DefaultConnection")).CreateConnection());
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

        //GetDataList的單元測試
        [Fact]
        public async Task GetAllDataTest()
        {
            //Arrange
            var mock = new Mock<IBaseDapper<ProjectDBContext_Default>>();
            mock.Setup(x => x.CreateConnection()).Returns(new ProjectDBContext_Default(Configuration.GetConnectionString("DefaultConnection")).CreateConnection());
            var service = new EnrollmentService(mock.Object);
            //Act
            var result = await service.GetDataList();
            //Assert
            Assert.NotNull(result);
        }
    }
}
