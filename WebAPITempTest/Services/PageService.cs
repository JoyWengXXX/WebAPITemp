using Moq;
using webAPITemp.Models.ViewModels;
using webAPITemp.Services.interfaces;
using Xunit;

namespace WebAPITemplateTest.Services
{
    public class PageService
    {
        //GetMenuPages的單元測試
        [Fact]
        public async Task GetMenuPages_ValidRoleID_ReturnMenuTreeViewModel()
        {
            //Arrange
            //mock一個IPageService物件
            var mockPageService = new Mock<IPageService>();
            //設定mock物件的GetMenuPages方法回傳List<MenuTreeViewModel>
            mockPageService.Setup(x => x.GetMenuPages(It.IsAny<int>())).ReturnsAsync(new List<MenuTreeViewModel>()
            {
                new MenuTreeViewModel()
                {
                    PageID = 1,
                    PageName = "Test",
                    ParentPageID = 0,
                    Level = 0
                }
            });
            //Act
            //呼叫mock物件的GetMenuPages方法
            var result = await mockPageService.Object.GetMenuPages(1);
            //Assert
            //驗證回傳值是否為List<MenuTreeViewModel>
            Assert.IsType<List<MenuTreeViewModel>>(result);
        }
    }
}
