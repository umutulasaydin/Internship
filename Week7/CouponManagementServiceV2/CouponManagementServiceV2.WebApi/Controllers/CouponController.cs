using CouponManagementServiceV2.Core.Business.Interfaces;
using CouponManagementServiceV2.Core.Model.Data;
using CouponManagementServiceV2.Core.Model.Shared;
using Microsoft.AspNetCore.Cors;
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
        public async Task<BaseResponse<CouponResponse>> GetCouponById(GetCoupon<int> id)
        {
            _logger.Info("Get coupon by id called");
            return await _queryService.GetCouponByIdRequest(id.input);
        }

        [HttpPost("GetSerieId")]
        public async Task<BaseResponse<IEnumerable<CouponResponse>>> GetCouponBySerieId(GetCoupon<string> id)
        {
            _logger.Info("Get coupons by serie id called");
            return await _queryService.GetCouponsBySerieIdRequest(id.input);
        }

        [HttpPost("GetUsername")]
        public async Task<BaseResponse<IEnumerable<CouponResponse>>> GetCouponByUsername(GetCoupon<string> username)
        {
            _logger.Info("Get coupons by username called");
            return await _queryService.GetCouponsByUsernameRequest(username.input);
        }

        [HttpPost("Redeem")]
        public async Task<BaseResponse<string>> RedeemCoupon(RedemptCoupon coupon)
        {
            _logger.Info("Redeem coupon called");
            Request.Headers.TryGetValue("token", out StringValues token);
            return await _commandService.RedeemCouponRequest(coupon, token);
        }

        [HttpPost("Void")]
        public async Task<BaseResponse<string>> VoidCoupon(RedemptCoupon coupon)
        {
            _logger.Info("Void coupon called");
            Request.Headers.TryGetValue("token", out StringValues token);
            return await _commandService.VoidCouponRequest(coupon, token);
        }


        [HttpPost("ChangeStatus")]
        public async Task<BaseResponse<string>> ChangeStatus(StatusCoupon coupon)
        {
            _logger.Info("Change status called");
            Request.Headers.TryGetValue("token", out StringValues token);
            return await _commandService.ChangeStatusRequest(coupon, token);
        }

        [HttpPost("GetCouponInfo")]
        public async Task<BaseResponse<IEnumerable<CouponLogResponse>>> GetCouponInfo(GetCoupon<int> id)
        {
            _logger.Info("Get coupon info called");
            return await _queryService.GetCouponInfoRequest(id.input);
        }


        [HttpPost("GetValidCoupons")]
        public async Task<BaseResponse<IEnumerable<CouponResponse>>> GetValidCoupons(BaseRequest request)
        {
            _logger.Info("Get valid coupons called");
            return await _queryService.GetValidCouponsRequest();
        }

        [HttpPost("GetAllCoupons")]
        public async Task<BaseResponse<PageInfo<IEnumerable<AllCouponResponse>>>> GetAllCoupons(AllCouponReqeust parameters)
        {
            _logger.Info("All coupons with parameters called");
            return await _queryService.GetAllCouponsRequest(parameters);
        }

        [HttpPost("GetAllCouponLogs")]
        public async Task<BaseResponse<PageInfo<IEnumerable<AllCouponLogResponse>>>> GetAllCouponLogs(AllCouponLogRequest parameters)
        {
            _logger.Info("All coupons with parameters called");
            return await _queryService.GetAllCouponLogsRequest(parameters);
        }

        [HttpPost("GetUserInfo")]
        public async Task<BaseResponse<Users>> GetUserInfo(BaseRequest request)
        {
            _logger.Info("Get user info called");
            Request.Headers.TryGetValue("token", out StringValues token);
            return await _queryService.GetUserInfoRequest(token);
        }

        [HttpPost("Dashboard")]
        public async Task<BaseResponse<Dashboard>> Dashboard(BaseRequest request)
        {
            _logger.Info("Dashboard called");
            return await _queryService.DashboardRequest();
        }

    }
}
