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
        public async Task<BaseResponse<Student>> Get(XRequest request) {
            _logger.LogInformation("Get function called. Dataset: " + request.dataset + " ClientCode: " + request.clientCode + " PosName: " + request.posName);
            return await request.getAll(_connectionHelper);
            
        }

        [HttpPost("{id}")]
        public async Task<BaseResponse<Student>> GetOne(XRequest request, int id)
        {
            _logger.LogInformation("Get One function called. Dataset: " + request.dataset + " ClientCode: " + request.clientCode + " PosName: " + request.posName);
            return await request.getOne(_connectionHelper, id);
        }

        [HttpPost("Add")]
        public async Task<BaseResponse<Student>> Add(AddItem item)
        {
            _logger.LogInformation("Add function called. Dataset: " + item.request.dataset + " ClientCode: " + item.request.clientCode + " PosName: " + item.request.posName);
            return await item.request.Insert(_connectionHelper, item.student);
        }
        
        [HttpDelete("{id}")]
        public async Task<BaseResponse<Student>> Delete(XRequest request, int id)
        {
            _logger.LogInformation("Delete function called. Dataset: " + request.dataset + " ClientCode: " + request.clientCode + " PosName: " + request.posName);
            return await request.Delete(_connectionHelper, id);
        }
        
    }
}
