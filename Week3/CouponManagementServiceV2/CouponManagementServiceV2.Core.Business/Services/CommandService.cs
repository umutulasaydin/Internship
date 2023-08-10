using CouponManagementServiceV2.Core.Business.Interfaces;
using CouponManagementServiceV2.Core.Data.Interfaces;
using CouponManagementServiceV2.Core.Model.Data;
using CouponManagementServiceV2.Core.Model.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementServiceV2.Core.Business.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandRepository _commandRepository;
        private readonly Cryption _crypte;
        private readonly IConfiguration _configuration;

        public CommandService(ICommandRepository commandRepository, IConfiguration configuration)
        {
            _commandRepository = commandRepository;
            _crypte = new Cryption();
            _configuration = configuration;
        }
        public async Task<BaseResponse<string>> SignUpRequest(Users item)
        {
            var result = await _commandRepository.SignUpOperation(item);
            if (result == 0 || result == -1)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -4,
                    errorMessage = "There is user exist in this username!",
                    result = "Signup Failed!"
                };
            }
            else if (result == -2)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Something happend while executing command",
                    result = "Signup Failed!"
                };
            }
            return new BaseResponse<string>
            {
                isSucces = true,
                statusCode = 1,
                errorMessage = "",
                result = "Signup Successfull"
            };
        }
        public async Task<BaseResponse<CouponResponse>> CreateCouponRequest(Coupons coupon, string token)
        {
            coupon.cpnCreatorId = _crypte.GetUserIdFromToken(token, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);
            if (coupon.cpnCreatorId == -1)
            {
                return new BaseResponse<CouponResponse>
                {
                    isSucces = false,
                    statusCode = -5,
                    errorMessage = "Token Validation Failed",
                    result = null
                };
            }
            
            var result = await _commandRepository.CreateCoupon(coupon);

            if (result <= 0)
            {
                return new BaseResponse<CouponResponse>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Something happend while executing command",
                    result = null
                };
            }
            CouponResponse response = new CouponResponse
            {
                cpnCode = coupon.cpnCode,
                cpnCreatorId = coupon.cpnCreatorId,
                cpnCurrentRedemptValue = coupon.cpnCurrentRedemptValue,
                cpnId = coupon.cpnId,
                cpnRedemptionLimit = coupon.cpnRedemptionLimit,
                cpnSerieId = coupon.cpnSerieId,
                cpnStartDate = coupon.cpnStartDate,
                cpnStatus = coupon.cpnStatus,
                cpnValidDate = coupon.cpnValidDate
            };
            return new BaseResponse<CouponResponse>
            {
                isSucces = true,
                statusCode = 1,
                errorMessage = "",
                result = response
            };
        }

        public async Task<BaseResponse<List<CouponResponse>>> CreateSerieCouponRequest(CouponSeries serie, string token)
        {
            serie.cpsUserId = _crypte.GetUserIdFromToken(token, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);
            if (serie.cpsUserId== -1)
            {
                return new BaseResponse<List<CouponResponse>>
                {
                    isSucces = false,
                    statusCode = -5,
                    errorMessage = "Token Validation Failed",
                    result = null
                };
            }

            var result = await _commandRepository.CreateSeriesCoupon(serie);

            if (result == null)
            {
                return new BaseResponse<List<CouponResponse>>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Something happend while executing command",
                    result = null
                };
            }
            return new BaseResponse<List<CouponResponse>>
            {
                isSucces = true,
                statusCode = 1,
                errorMessage = "",
                result = result
            };
        }
    }
}
