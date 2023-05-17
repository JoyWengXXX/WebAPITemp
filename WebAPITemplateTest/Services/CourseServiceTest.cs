using Moq;
using WebAPITemplete.Models.DTOs.DefaultDB;
using WebAPITemplete.Services.interfaces;
using Xunit;

namespace WebAPITemplateTest.Services
{
    public class CourseServiceTest
    {
        ////CreateData的單元測試
        //[Fact]
        //public async Task CreateData_ValidCustomer_ReturnCreatedNum()
        //{
        //    //Arrange
        //    //mock一個ICourseService物件
        //    var mockCourseService = new Mock<ICourseService>();
        //    //設定mock物件的CreateData方法回傳1
        //    mockCourseService.Setup(x => x.CreateData(It.IsAny<CourseDTO>())).ReturnsAsync(1);
        //    //建立CourseDTO物件
        //    var input = new CourseDTO()
        //    {
        //        Name = "Test",
        //        Descript = "Test"
        //    };
        //    //Act
        //    //呼叫mock物件的CreateData方法
        //    var result = await mockCourseService.Object.CreateData(input);
        //    //Assert
        //    //驗證回傳值是否為1
        //    Assert.Equal(1, result);
        //}

        ////DeleteData的單元測試
        //[Theory]
        //[InlineData(1)]
        //public async Task DeleteData_ValidCustomer_ReturnDeletedNum(int id)
        //{
        //    //Arrange
        //    //mock一個ICourseService物件
        //    var mockCourseService = new Mock<ICourseService>();
        //    //設定mock物件的DeleteData方法回傳1
        //    mockCourseService.Setup(x => x.DeleteData(It.IsAny<CourseDTO>())).ReturnsAsync(1);
        //    //建立CourseDTO物件
        //    var input = new CourseDTO()
        //    {
        //        Id = id
        //    };
        //    //Act
        //    //呼叫mock物件的DeleteData方法
        //    var result = await mockCourseService.Object.DeleteData(input);
        //    //Assert
        //    //驗證回傳值是否為1
        //    Assert.Equal(1, result);
        //}

        ////UpdateData的單元測試
        //[Theory]
        //[InlineData(1, "Test", "Test")]
        //public async Task UpdateData_ValidCustomer_ReturnUpdatedNum(int id, string name, string descript)
        //{
        //    //Arrange
        //    //mock一個ICourseService物件
        //    var mockCourseService = new Mock<ICourseService>();
        //    //設定mock物件的UpdateData方法回傳1
        //    mockCourseService.Setup(x => x.UpdateData(It.IsAny<CourseDTO>())).ReturnsAsync(1);
        //    //建立CourseDTO物件
        //    var input = new CourseDTO()
        //    {
        //        Id = id,
        //        Name = name,
        //        Descript = descript
        //    };
        //    //Act
        //    //呼叫mock物件的UpdateData方法
        //    var result = await mockCourseService.Object.UpdateData(input);
        //    //Assert
        //    //驗證回傳值是否為1
        //    Assert.Equal(1, result);
        //}

        ////GetDataList的單元測試
        //[Fact]
        //public async Task GetDataList_ValidCustomer_ReturnCourseDTOList()
        //{
        //    //Arrange
        //    //mock一個ICourseService物件
        //    var mockCourseService = new Mock<ICourseService>();
        //    //設定mock物件的GetDataList方法回傳CourseDTO List
        //    mockCourseService.Setup(x => x.GetDataList()).ReturnsAsync(new List<CourseDTO>()
        //    {
        //        new CourseDTO()
        //        {
        //            Id = 1,
        //            Name = "Test",
        //            Descript = "Test"
        //        }
        //    });
        //    //Act
        //    //呼叫mock物件的GetDataList方法
        //    var result = await mockCourseService.Object.GetDataList();
        //    //Assert
        //    //驗證回傳值是否為CourseDTO List
        //    Assert.IsType<List<CourseDTO>>(result);
        //}

        ////GetDataById的單元測試
        //[Theory]
        //[InlineData(1)]
        //public async Task GetDataById_ValidCustomer_ReturnCourseDTO(int id)
        //{
        //    //Arrange
        //    //mock一個ICourseService物件
        //    var mockCourseService = new Mock<ICourseService>();
        //    //建立CourseDTO物件
        //    var input = new CourseDTO()
        //    {
        //        Id = id,
        //        Name = "Test",
        //        Descript = "Test"
        //    };
        //    //設定mock物件的GetExistedData方法回傳CourseDTO
        //    mockCourseService.Setup(x => x.GetExistedData(It.IsAny<CourseDTO>())).ReturnsAsync(input);
        //    //Act
        //    //呼叫mock物件的GetExistedData方法
        //    var result = await mockCourseService.Object.GetExistedData(input);
        //    //Assert
        //    //驗證回傳值是否與input相同
        //    Assert.Equal(result, input);
        //}
    }
}
