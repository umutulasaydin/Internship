using CouponManagementServiceV2.Core.Business.Interfaces;
using CouponManagementServiceV2.Core.Data.Interfaces;
using CouponManagementServiceV2.Core.Model.Data;
using CouponManagementServiceV2.Core.Model.Shared;
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
        public async Task<BaseResponse<Coupons>> CreateCouponRequest(Coupons coupon, string token, Status status)
        {
            IEnumerable<Claim> claims = _crypte.GetInfoFromToken(token, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);
            if (claims == null)
            {
                return new BaseResponse<Coupons>
                {
                    isSucces = false,
                    statusCode = -5,
                    errorMessage = "Token Validation Failed",
                    result = null
                };
            }
            coupon.cpnId = Int32.Parse(claims.First(claim => claim.Type == "PrimarySid").Value);
            coupon.cpnCode = Guid.NewGuid().ToString();
            coupon.cpnStatus = (int)status;
            var result = await _commandRepository.CreateCoupon(coupon);

            if (result == 0 || result == -1 || result == -2)
            {
                return new BaseResponse<Coupons>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Something happend while executing command",
                    result = null
                };
            }
            return new BaseResponse<Coupons>
            {
                isSucces = true,
                statusCode = 1,
                errorMessage = "",
                result = coupon
            };
        }
    }
}
