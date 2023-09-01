using CouponManagementServiceV2.Core.Business.Interfaces;
using CouponManagementServiceV2.Core.Model.Data;
using CouponManagementServiceV2.Core.Model.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace CouponManagementServiceV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly Logger _logger;
        private readonly ICommandService _commandService;
        private readonly IQueryService _queryService;

        public AuthController(ICommandService commandService, IQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
            _logger = LogManager.GetCurrentClassLogger();
       
        }

        [HttpPost("Signup")]
        public async Task<BaseResponse<string>> SignUp(Users item)
        {
            _logger.Info("SignUp called!");
            return await _commandService.SignUpRequest(item);

        }

        [HttpPost("Login")]
        public async Task<BaseResponse<string>> Login(Login item)
        {
            _logger.Info("Login called!");
            return await _queryService.LoginRequest(item);
        }

    }
}
