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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentServices;
        private readonly IAPIResponceAdapter _httpResponceAdapter;

        public StudentController(IStudentService StudentServices, IAPIResponceAdapter httpResponceAdapter)
        {
            _studentServices = StudentServices;
            _httpResponceAdapter = httpResponceAdapter;
        }

        /// <summary>
        /// 取得學生資料清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetStudents()
        {
            IEnumerable<StudentDTO>? result = await _studentServices.GetDataList();
            if(result == null)
                return _httpResponceAdapter.Fail("無資料");
            else
                return _httpResponceAdapter.Ok(result);
        }
        

        /// <summary>
        /// 取得指定ID的學生資料
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetStudent(int Id)
        {
            StudentDTO? result = await _studentServices.GetExistedData(new StudentDTO() { Id = Id });
            if(result == null)
                return _httpResponceAdapter.Fail("無資料");
            else
                return _httpResponceAdapter.Ok(result);
        }

        /// <summary>
        /// 新增學生資料
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateStudent(StudentDTO Input)
        {
            if(await _studentServices.CreateData(Input) > 0)
                return _httpResponceAdapter.Ok("新增成功");
            else
                return _httpResponceAdapter.Fail("新增失敗");
        }

        /// <summary>
        /// 修改學生資料
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateStudent(StudentDTO Input)
        {
            if(await _studentServices.UpdateData(Input) > 0)
                return _httpResponceAdapter.Ok("更新成功");
            else
                return _httpResponceAdapter.Fail("更新失敗");
        }

        /// <summary>
        /// 刪除學生資料
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            if(await _studentServices.DeleteData(new StudentDTO() { Id = Id }) > 0)
                return _httpResponceAdapter.Ok("刪除成功");
            else
                return _httpResponceAdapter.Fail("刪除失敗");
        }
    }
}
