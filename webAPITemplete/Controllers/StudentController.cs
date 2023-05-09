using webAPITemplete.Services.interfaces;
using CommomLibrary.AppInterfaceAdapters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webAPITemplete.Models.DTOs;

namespace webAPITemplete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentServices;

        public StudentController(IStudentService StudentServices)
        {
            _studentServices = StudentServices;
        }

        /// <summary>
        /// 取得學生資料清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetStudents()
        {
            List<Student>? result = await _studentServices.GetDataList();
            if(result == null)
                return HttpResponceAdapter.Fail("無資料");
            else
                return HttpResponceAdapter.Ok(result);
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
            Student? result = await _studentServices.GetExistedData(new Student() { Id = Id });
            if(result == null)
                return HttpResponceAdapter.Fail("無資料");
            else
                return HttpResponceAdapter.Ok(result);
        }

        /// <summary>
        /// 新增學生資料
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateStudent(Student Input)
        {
            if(await _studentServices.CreateData(Input))
                return HttpResponceAdapter.Ok("新增成功");
            else
                return HttpResponceAdapter.Fail("新增失敗");
        }

        /// <summary>
        /// 修改學生資料
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateStudent(Student Input)
        {
            if(await _studentServices.UpdateData(Input))
                return HttpResponceAdapter.Ok("更新成功");
            else
                return HttpResponceAdapter.Fail("更新失敗");
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
            if(await _studentServices.DeleteData(new Student() { Id = Id }))
                return HttpResponceAdapter.Ok("刪除成功");
            else
                return HttpResponceAdapter.Fail("刪除失敗");
        }
    }
}
