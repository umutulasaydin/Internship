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
                    errorMessage = "Invalid username or password",
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
    }
}
