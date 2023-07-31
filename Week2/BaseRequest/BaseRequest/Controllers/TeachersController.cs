using BaseRequest.BaseRequest;
using BaseRequest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : TemplateController<BaseResponse<Teacher>,Teacher>
    {
        public TeachersController(BaseRequest<BaseResponse<Teacher>,Teacher> BaseRequestService) : base(BaseRequestService, "Teacher")
        {

        }
    }
}
