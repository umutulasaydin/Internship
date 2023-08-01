using BaseRequest.Helpers;
using BaseRequest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BaseRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<XRequest> _logger;
        private readonly ConnectionHelper _connectionHelper;
        public StudentController(ILogger<XRequest> logger, ConnectionHelper connectionHelper) {
            _logger = logger;
            _connectionHelper = connectionHelper;
        }

        [HttpPost]
        public BaseResponse<Student> Get(XRequest request) {
            _logger.LogInformation("Get function called. Dataset: " + request.dataset + " ClientCode: " + request.clientCode + " PosName: " + request.posName);
            return request.get(_connectionHelper);
            
        }
    }
}
