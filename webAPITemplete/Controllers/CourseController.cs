using webAPITemplete.Services.interfaces;
using webAPITemplete.AppInterfaceAdapters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webAPITemplete.Models.DTOs.DefaultDB;
using webAPITemplete.AppInterfaceAdapters.interfaces;

namespace webAPITemplete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseServices;
        private readonly IAPIResponceAdapter _httpResponceAdapter;

        public CourseController(ICourseService CourseServices, IAPIResponceAdapter httpResponceAdapter)
        {
            _courseServices = CourseServices;
            _httpResponceAdapter = httpResponceAdapter;
        }

        /// <summary>
        ///  取得課程資料清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetCourses()
        {
            IEnumerable<CourseDTO>? result = await _courseServices.GetDataList();
            if (result == null)
                return _httpResponceAdapter.Fail("查無資料");
            else
                return _httpResponceAdapter.Ok(result);
        }

        /// <summary>
        ///  取得指定ID的課程資料
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetCourse(int Id)
        {
            CourseDTO? result = await _courseServices.GetExistedData(new CourseDTO() { Id = Id });
            if (result == null)
                return _httpResponceAdapter.Fail("查無此資料");
            else
                return _httpResponceAdapter.Ok(result);
        }

        /// <summary>
        ///  新增課程資料
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCourse(CourseDTO Input)
        {
            if(await _courseServices.CreateData(Input) > 0)
                return _httpResponceAdapter.Ok("新增成功");
            else
                return _httpResponceAdapter.Fail("新增失敗");
        }

        /// <summary>
        ///  更新課程資料
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCourse(CourseDTO Input)
        {
            if(await _courseServices.UpdateData(Input) > 0)
                return _httpResponceAdapter.Ok("更新成功");
            else
                return _httpResponceAdapter.Fail("更新失敗");
        }

        /// <summary>
        ///  刪除課程資料
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCourse(int Id)
        {
            if(await _courseServices.DeleteData(new CourseDTO() { Id = Id }) > 0)
                return _httpResponceAdapter.Ok("刪除成功");
            else
                return _httpResponceAdapter.Fail("刪除失敗");
        }
    }
}
