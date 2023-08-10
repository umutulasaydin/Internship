using CouponManagementServiceV2.Core.Business.Interfaces;
using CouponManagementServiceV2.Core.Data.Interfaces;
using CouponManagementServiceV2.Core.Model.Data;
using CouponManagementServiceV2.Core.Model.Shared;
using Microsoft.Extensions.Configuration;

namespace CouponManagementServiceV2.Core.Business.Services
{
    public class QueryService : IQueryService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly Cryption _crypte;
        private readonly IConfiguration _configuration;

        public QueryService(IQueryRepository queryRepository, IConfiguration configuration)
        {
            _queryRepository = queryRepository;
            _crypte = new Cryption();
            _configuration = configuration;
        }

        public async Task<BaseResponse<string>> LoginRequest(Login item)
        {
            var entity = await _queryRepository.LoginOperation(item);
            if (entity == null)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -2,
                    errorMessage = "Invalid username or password",
                    result = ""
                };
            }
            var token = _crypte.GenerateJwtToken(entity, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);

            return new BaseResponse<string>
            {
                isSucces = true,
                statusCode = 1,
                errorMessage = "",
                result = token
            };
        }

        public async Task<BaseResponse<CouponResponse>> GetCouponByIdRequest(int id, string token)
        {
            var user = _crypte.GetUserIdFromToken(token, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);
            if (user == -1)
            {
                return new BaseResponse<CouponResponse>
                {
                    isSucces = false,
                    statusCode = -5,
                    errorMessage = "Token Validation Failed",
                    result = null
                };
            }
            var entity = await _queryRepository.GetCouponById(id);
            if (entity == null)
            {
                return new BaseResponse<CouponResponse>
                {
                    isSucces = false,
                    statusCode = -2,
                    errorMessage = "There is no coupon with this id",
                    result = null
                };
            }
            return new BaseResponse<CouponResponse>
            {
                isSucces = true,
                statusCode = 1,
                errorMessage = "",
                result = entity
            };
        }

        public async Task<BaseResponse<IEnumerable<CouponResponse>>> GetCouponsBySerieIdRequest(string id, string token)
        {
            var user = _crypte.GetUserIdFromToken(token, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);
            if (user == -1)
            {
                return new BaseResponse<IEnumerable<CouponResponse>>
                {
                    isSucces = false,
                    statusCode = -5,
                    errorMessage = "Token Validation Failed",
                    result = null
                };
            }
            var entity = await _queryRepository.GetCouponsBySerieId(id);
            if (entity == null || entity.Count() == 0)
            {
                return new BaseResponse<IEnumerable<CouponResponse>>
                {
                    isSucces = false,
                    statusCode = -2,
                    errorMessage = "There is no coupon with this serie id",
                    result = null
                };
            }
            return new BaseResponse<IEnumerable<CouponResponse>>
            {
                isSucces = true,
                statusCode = 1,
                errorMessage = "",
                result = entity
            };
        }

        public async Task<BaseResponse<IEnumerable<CouponResponse>>> GetCouponsByUsernameRequest(string username, string token)
        {
            var user = _crypte.GetUserIdFromToken(token, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);
            if (user == -1)
            {
                return new BaseResponse<IEnumerable<CouponResponse>>
                {
                    isSucces = false,
                    statusCode = -5,
                    errorMessage = "Token Validation Failed",
                    result = null
                };
            }
            var entity = await _queryRepository.GetCouponsByUsername(username);
            if (entity == null || entity.Count() == 0)
            {
                return new BaseResponse<IEnumerable<CouponResponse>>
                {
                    isSucces = false,
                    statusCode = -2,
                    errorMessage = "There is no coupon with this serie id",
                    result = null
                };
            }
            return new BaseResponse<IEnumerable<CouponResponse>>
            {
                isSucces = true,
                statusCode = 1,
                errorMessage = "",
                result = entity
            };
        }
    }
}
