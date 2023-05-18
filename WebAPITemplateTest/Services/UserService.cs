using Moq;
using WebAPITemplete.Models.DTOs.DefaultDB;
using WebAPITemplete.Services.interfaces;
using Xunit;

namespace WebAPITemplateTest.Services
{
    public class UserService
    {
        //GetUser的單元測試
        [Fact]
        public async Task GetUser_ValidUserID_ReturnUserDTO()
        {
            //Arrange
            //mock一個IUserService物件
            var mockUserService = new Mock<IUserService>();
            //設定mock物件的GetUserInfo方法回傳UserDTO
            mockUserService.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new UserDTO()
            {
                SerialNum = 1,
                UserID = "Test",
                FirstName = "Test",
                LastName = "Test",
                RoleID = 1,
                RoleName = "Test"
            });
            //Act
            //呼叫mock物件的GetUserInfo方法
            var result = await mockUserService.Object.GetUser("Test", "Test");
            //Assert
            //驗證回傳值是否為UserDTO
            Assert.IsType<UserDTO>(result);
        }
    }
}
