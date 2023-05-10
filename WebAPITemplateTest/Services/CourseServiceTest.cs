using Moq;
using webAPITemplete.Models.DTOs.DefaultDB;
using webAPITemplete.Repository.Dapper.DbContexts;
using webAPITemplete.Repository.Dapper.interfaces;
using webAPITemplete.Services;
using Xunit;

namespace WebAPITemplateTest.Services
{
    public class CourseServiceTest
    {
        //CreateData的單元測試
        [Fact]
        public async Task CreateDataTest()
        {
            //Arrange
            var mock = new Mock<IBaseDapper<CourseDTO, ProjectDBContext_Default>>();
            var mock1 = new Mock<IBaseDapper<CourseDTO, ProjectDBContext_Test1>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<CourseDTO>())).ReturnsAsync(1);
            mock1.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<CourseDTO>())).ReturnsAsync(1);
            var service = new CourseService(mock.Object,mock1.Object);
            var input = new CourseDTO()
            {
                Name = "Test",
                Descript = "Test"
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
            var mock = new Mock<IBaseDapper<CourseDTO, ProjectDBContext_Default>>();
            var mock1 = new Mock<IBaseDapper<CourseDTO, ProjectDBContext_Test1>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<CourseDTO>())).ReturnsAsync(1);
            mock1.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<CourseDTO>())).ReturnsAsync(1);
            var service = new CourseService(mock.Object, mock1.Object);
            var input = new CourseDTO()
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
        [InlineData(1, "Test", "Test")]
        [InlineData(2, "Test", "")]
        [InlineData(3, "", "Test")]
        public async Task UpdateDataTest(int id, string name, string descript)
        {
            //Arrange
            var mock = new Mock<IBaseDapper<CourseDTO, ProjectDBContext_Default>>();
            var mock1 = new Mock<IBaseDapper<CourseDTO, ProjectDBContext_Test1>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<CourseDTO>())).ReturnsAsync(1);
            mock1.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<CourseDTO>())).ReturnsAsync(1);
            var service = new CourseService(mock.Object, mock1.Object);
            var input = new CourseDTO()
            {
                Id = id,
                Name = name,
                Descript = descript
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
            var mock = new Mock<IBaseDapper<CourseDTO, ProjectDBContext_Default>>();
            var mock1 = new Mock<IBaseDapper<CourseDTO, ProjectDBContext_Test1>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<CourseDTO>())).ReturnsAsync(1);
            mock1.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<CourseDTO>())).ReturnsAsync(1);
            var service = new CourseService(mock.Object, mock1.Object);
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
            var mock = new Mock<IBaseDapper<CourseDTO, ProjectDBContext_Default>>();
            var mock1 = new Mock<IBaseDapper<CourseDTO, ProjectDBContext_Test1>>();
            mock.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<CourseDTO>())).ReturnsAsync(1);
            mock1.Setup(x => x.ExecuteCommand(It.IsAny<string>(), It.IsAny<CourseDTO>())).ReturnsAsync(1);
            var service = new CourseService(mock.Object, mock1.Object);
            var input = new CourseDTO()
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
