using CouponManagementServiceV2.Core.Business.Interfaces;
using CouponManagementServiceV2.Core.Model.Data;
using CouponManagementServiceV2.Core.Model.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using NLog;

namespace CouponManagementServiceV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly Logger _logger;
        private readonly ICommandService _commandService;
        private readonly IQueryService _queryService;

        public CouponController(ICommandService commandService, IQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost("Create")]
        public async Task<BaseResponse<CouponResponse>> CreateCoupon(Coupons coupon)
        {
            _logger.Info("Create coupon request called");
            Request.Headers.TryGetValue("token", out StringValues token);
            return await _commandService.CreateCouponRequest(coupon, token);
        }

        [HttpPost("Serie")]
        public async Task<BaseResponse<List<CouponResponse>>> CreateSerieCoupon(CouponSeries serie)
        {
            _logger.Info("Create serie coupon called");
            Request.Headers.TryGetValue("token", out StringValues token);
            return await _commandService.CreateSerieCouponRequest(serie, token);
        }

        [HttpPost("Get")]
        public async Task<BaseResponse<CouponResponse>> GetCouponById([FromBody] int id)
        {
            _logger.Info("Get coupon by id called");
            Request.Headers.TryGetValue("token", out StringValues token);
            return await _queryService.GetCouponByIdRequest(id, token);
        }

        [HttpPost("GetSerieId")]
        public async Task<BaseResponse<IEnumerable<CouponResponse>>> GetCouponBySerieId([FromBody] string id)
        {
            _logger.Info("Get coupons by serie id");
            Request.Headers.TryGetValue("token", out StringValues token);
            return await _queryService.GetCouponsBySerieIdRequest(id, token);
        }

        [HttpPost("GetUsername")]
        public async Task<BaseResponse<IEnumerable<CouponResponse>>> GetCouponByUsername([FromBody] string username)
        {
            _logger.Info("Get coupons by username");
            Request.Headers.TryGetValue("token", out StringValues token);
            return await _queryService.GetCouponsByUsernameRequest(username, token);
        }
    }
}
