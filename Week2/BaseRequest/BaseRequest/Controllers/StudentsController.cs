using BaseRequest.BaseRequest;
using BaseRequest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : TemplateController<BaseResponse<Student>, Student>
    {
        public StudentsController(BaseRequest<BaseResponse<Student>, Student> BaseRequestService) : base(BaseRequestService, "Student")
        {
        
        }
    }
}
