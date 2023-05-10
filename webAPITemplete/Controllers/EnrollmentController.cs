using webAPITemplete.Services.interfaces;
using CommomLibrary.AppInterfaceAdapters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webAPITemplete.Models.DTOs;
using webAPITemplete.Models.DTOs.DefaultDB;

namespace webAPITemplete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentServices;
        private readonly IStudentService _studentServices;
        private readonly ICourseService _courseServices;

        public EnrollmentController(IEnrollmentService EnrollmentServices, IStudentService StudentServices, ICourseService CourseServices)
        {
            _enrollmentServices = EnrollmentServices;
            _studentServices = StudentServices;
            _courseServices = CourseServices;
        }

        /// <summary>
        /// 取得註冊資料清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetEnrollments()
        {
            IEnumerable<EnrollmentDTO>? result = await _enrollmentServices.GetDataList();
            if (result == null)
                return HttpResponceAdapter.Fail("查無資料");
            else
                return HttpResponceAdapter.Ok(result);
        }

        /// <summary>
        /// 取得指定ID的註冊資料
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetEnrollment(int Id)
        {
            EnrollmentDTO? result = await _enrollmentServices.GetExistedData(new EnrollmentDTO() { Id = Id });
            if (result == null)
                return HttpResponceAdapter.Fail("查無此資料");
            else
                return HttpResponceAdapter.Ok(result);
        }

        /// <summary>
        /// 新增註冊資料
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateEnrollment(EnrollmentDTO Input)
        {
            //檢查Input的Student_Id、Course_Id是否存在
            if (await _studentServices.GetExistedData(new StudentDTO() { Id = Input.Student_Id }) == null || await _courseServices.GetExistedData(new CourseDTO() { Id = Input.Course_Id }) == null)
                return HttpResponceAdapter.Fail("查無此學生/課程ID");

            if (await _enrollmentServices.CreateData(Input) > 0)
                return HttpResponceAdapter.Ok("新增成功");
            else
                return HttpResponceAdapter.Fail("新增失敗");
        }

        /// <summary>
        ///  更新註冊資料
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateEnrollment(EnrollmentDTO Input)
        {
            //檢查Input的Student_Id、Course_Id是否存在
            if (await _studentServices.GetExistedData(new StudentDTO() { Id = Input.Student_Id }) == null || await _courseServices.GetExistedData(new CourseDTO() { Id = Input.Course_Id }) == null)
                return HttpResponceAdapter.Fail("查無此學生/課程ID");

            if (await _enrollmentServices.UpdateData(Input) > 0)
                return HttpResponceAdapter.Ok("更新成功");
            else
                return HttpResponceAdapter.Fail("更新失敗");
        }

        /// <summary>
        ///  刪除註冊資料
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteEnrollment(int Id)
        {
            if(await _enrollmentServices.DeleteData(new EnrollmentDTO() { Id = Id }) > 0)
                return HttpResponceAdapter.Ok("刪除成功");
            else
                return HttpResponceAdapter.Fail("刪除失敗");
        }
    }
}
