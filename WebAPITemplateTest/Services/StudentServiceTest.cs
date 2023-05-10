using Moq;
using webAPITemplete.Models.DTOs.DefaultDB;
using webAPITemplete.Repository.Dapper.DbContexts;
using webAPITemplete.Repository.Dapper.interfaces;
using webAPITemplete.Services;
using Xunit;

namespace WebAPITemplateTest.Services
{
    public class StudentServiceTest
    {
        //CreateData的單元測試
        [Fact]
        public async Task CreateDataTest()
        {
            //Arrange
            var mock = new Mock<IBaseDapper<StudentDTO, ProjectDBContext_Default>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<StudentDTO>())).ReturnsAsync(1);
            var service = new StudentService(mock.Object);
            var input = new StudentDTO()
            {
                Name = "Test",
                Email = "Test", 
                Address = "Test",
                Phone = "Test",
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
            var mock = new Mock<IBaseDapper<StudentDTO, ProjectDBContext_Default>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<StudentDTO>())).ReturnsAsync(1);
            var service = new StudentService(mock.Object);
            var input = new StudentDTO()
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
        [InlineData(1, "Test", "Test", "Test", "Test")]
        [InlineData(2, "Test", "Test", "Test", "")]
        [InlineData(3, "", "Test", "Test", "Test")]
        public async Task UpdateDataTest(int id, string name, string email, string address, string phone)
        {
            //Arrange
            var mock = new Mock<IBaseDapper<StudentDTO, ProjectDBContext_Default>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<StudentDTO>())).ReturnsAsync(1);
            var service = new StudentService(mock.Object);
            var input = new StudentDTO()
            {
                Id = id,
                Name = name,
                Email = email,
                Address = address,
                Phone = phone
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
            var mock = new Mock<IBaseDapper<StudentDTO, ProjectDBContext_Default>>();
            mock.Setup(x => x.QueryListData(It.IsAny<string>())).ReturnsAsync(new List<StudentDTO>());
            var service = new StudentService(mock.Object);
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
            var mock = new Mock<IBaseDapper<StudentDTO, ProjectDBContext_Default>>();
            mock.Setup(x => x.QuerySingleData(It.IsAny<string>(), It.IsAny<StudentDTO>())).ReturnsAsync(new StudentDTO());
            var service = new StudentService(mock.Object);
            var input = new StudentDTO()
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
