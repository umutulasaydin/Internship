using CouponManagementServiceV2.Core.Business.Interfaces;
using CouponManagementServiceV2.Core.Data.Interfaces;
using CouponManagementServiceV2.Core.Model.Data;
using CouponManagementServiceV2.Core.Model.Shared;
using CouponManagementServiceV2.Core.Model.Shared.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace CouponManagementServiceV2.Core.Business.Services
{
    public class QueryService : IQueryService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly Cryption _crypte;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<Error> _errorLocalizer;
        private readonly IStringLocalizer<ErrorCode> _errorCodeLocalizer;


        public QueryService(IQueryRepository queryRepository, IConfiguration configuration, IStringLocalizer<Error> errorLocalizer, IStringLocalizer<ErrorCode> errorCodeLocalizer)
        {
            _queryRepository = queryRepository;
            _crypte = new Cryption();
            _configuration = configuration;
            _errorLocalizer = errorLocalizer;
            _errorCodeLocalizer = errorCodeLocalizer;
           
        }

        public async Task<BaseResponse<string>> LoginRequest(Login item)
        {
            var entity = await _queryRepository.LoginOperation(item);
            if (entity == null)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["INVALID_LOGIN"],
                    errorMessage = _errorLocalizer["INVALID_LOGIN"],
                    result = ""
                };
            }
            var token = _crypte.GenerateJwtToken(entity, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);
          
            return new BaseResponse<string>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = token
            };
        }

        public async Task<BaseResponse<CouponResponse>> GetCouponByIdRequest(int id)
        {
            
            var entity = await _queryRepository.GetCouponById(id);
            if (entity == null)
            {
                return new BaseResponse<CouponResponse>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_COUPON_ID"],
                    errorMessage = _errorLocalizer["NO_COUPON_ID"],
                    result = null
                };
            }
            

            return new BaseResponse<CouponResponse>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = entity
            };
        }

        public async Task<BaseResponse<IEnumerable<CouponResponse>>> GetCouponsBySerieIdRequest(string id)
        {
            
            var entity = await _queryRepository.GetCouponsBySerieId(id);
            if (entity == null || entity.Count() == 0)
            {
                return new BaseResponse<IEnumerable<CouponResponse>>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_COUPON_SERIE"],
                    errorMessage = _errorLocalizer["NO_COUPON_SERIE"],
                    result = null
                };
            }

            return new BaseResponse<IEnumerable<CouponResponse>>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = entity
            };
        }

        public async Task<BaseResponse<IEnumerable<CouponResponse>>> GetCouponsByUsernameRequest(string username)
        {
            
            var entity = await _queryRepository.GetCouponsByUsername(username);
            if (entity == null || entity.Count() == 0)
            {
                return new BaseResponse<IEnumerable<CouponResponse>>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_COUPON_USERNAME"],
                    errorMessage = _errorLocalizer["NO_COUPON_USERNAME"],
                    result = null
                };
            }

            return new BaseResponse<IEnumerable<CouponResponse>>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = entity
            };
        }

        public async Task<BaseResponse<IEnumerable<CouponLogResponse>>> GetCouponInfoRequest(int id)
        {
            var entity = await _queryRepository.GetCouponInfo(id);
            if (entity == null || entity.Count() == 0)
            {
                return new BaseResponse<IEnumerable<CouponLogResponse>>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_COUPON_ID"],
                    errorMessage = _errorLocalizer["NO_COUPON_ID"],
                    result = null
                };
            }

            return new BaseResponse<IEnumerable<CouponLogResponse>>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = entity
            };
        }

        public async Task<BaseResponse<IEnumerable<CouponLogResponse>>> GetCouponInfoByUserIdRequest(int id)
        {
            var entity = await _queryRepository.GetCouponInfoByUserId(id);
            if (entity == null || entity.Count() == 0)
            {
                return new BaseResponse<IEnumerable<CouponLogResponse>>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_USER_ACTIVITY"],
                    errorMessage = _errorLocalizer["NO_USER_ACTIVITY"],
                    result = null
                };
            }

            return new BaseResponse<IEnumerable<CouponLogResponse>>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = entity
            };
        }

        public async Task<BaseResponse<IEnumerable<CouponResponse>>> GetValidCouponsRequest()
        {
            var entity = await _queryRepository.GetValidCoupons();
            if (entity == null || entity.Count() == 0)
            {
                return new BaseResponse<IEnumerable<CouponResponse>>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_COUPON_VALID"],
                    errorMessage = _errorLocalizer["NO_COUPON_VALID"],
                    result = null
                };
            }

            return new BaseResponse<IEnumerable<CouponResponse>>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = entity
            };
        }

        public async Task<BaseResponse<PageInfo<IEnumerable<AllCouponResponse>>>> GetAllCouponsRequest(AllCouponReqeust parameters)
        {
            var entity = await _queryRepository.GetAllCoupons(parameters);
            if (entity == null)
            {
                return new BaseResponse<PageInfo<IEnumerable<AllCouponResponse>>>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_COUPON"],
                    errorMessage = _errorLocalizer["NO_COUPON"],
                    result = null
                };
            }

            return new BaseResponse<PageInfo<IEnumerable<AllCouponResponse>>>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = entity
            };
        }

        public async Task<BaseResponse<PageInfo<IEnumerable<AllCouponLogResponse>>>> GetAllCouponLogsRequest(AllCouponLogRequest parameters)
        {
            var entity = await _queryRepository.GetAllCouponLogs(parameters);
            if (entity == null)
            {
                return new BaseResponse<PageInfo<IEnumerable<AllCouponLogResponse>>>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_COUPON"],
                    errorMessage = _errorLocalizer["NO_COUPON"],
                    result = null
                };
            }

            return new BaseResponse<PageInfo<IEnumerable<AllCouponLogResponse>>>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = entity
            };
        }

        public async Task<BaseResponse<Users>> GetUserInfoRequest(string token)
        {
            var id = _crypte.GetUserIdFromToken(token, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);
            var entity = await _queryRepository.GetUserInfo(id);
            if (entity == null)
            {
                return new BaseResponse<Users>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_USER"],
                    errorMessage = _errorLocalizer["NO_USER"],
                    result = null
                };
            }

            return new BaseResponse<Users>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = entity
            };
        }

        public async Task<BaseResponse<Dashboard>> DashboardRequest()
        {
            var entity = await _queryRepository.Dashboard();
            if (entity == null)
            {
                return new BaseResponse<Dashboard>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["PROCESS_FAIL"],
                    errorMessage = _errorLocalizer["PROCESS_FAIL"],
                    result = null
                };
            }

            return new BaseResponse<Dashboard>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = entity
            };
        }
    }
}
