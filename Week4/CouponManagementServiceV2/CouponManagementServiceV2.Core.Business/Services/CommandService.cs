using CouponManagementServiceV2.Core.Business.Interfaces;
using CouponManagementServiceV2.Core.Data.Interfaces;
using CouponManagementServiceV2.Core.Model.Data;
using CouponManagementServiceV2.Core.Model.Shared;

using Microsoft.Extensions.Configuration;


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

            coupon.cpnId = await _commandRepository.CreateCoupon(coupon);

            if (coupon.cpnId <= 0)
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

        public async Task<BaseResponse<string>> RedeemCouponRequest(RedemptCoupon coupon, string token)
        {
            int uid = _crypte.GetUserIdFromToken(token, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);
            

            var result = await _commandRepository.RedeemCoupon(coupon, uid);

            if (result == -1)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -6,
                    errorMessage = "Not enough limit.",
                    result = ""
                };
            }
            else if (result == -2)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Coupon is used",
                    result = ""
                };
            }
            else if (result == -3)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Coupon is blocked",
                    result = ""
                };
            }
            else if (result == -4)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Coupon is drafted",
                    result = ""
                };
            }
            else if (result == -5)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -7,
                    errorMessage = "Valid date is expired",
                    result = ""
                };
            }
            else if (result == -6)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -2,
                    errorMessage = "Something happend while executing command",
                    result = ""
                };
            }

            return new BaseResponse<string>
            {
                isSucces = true,
                statusCode = 1,
                errorMessage = "",
                result = "Coupon redeemed"
            };
        }
        public async Task<BaseResponse<string>> VoidCouponRequest(RedemptCoupon coupon, string token)
        {
            int uid = _crypte.GetUserIdFromToken(token, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);
            

            var result = await _commandRepository.VoidCoupon(coupon, uid);

            if (result == -1)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -6,
                    errorMessage = "Not enough limit.",
                    result = ""
                };
            }
            else if (result == -2)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Coupon is used",
                    result = ""
                };
            }
            else if (result == -3)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Coupon is blocked",
                    result = ""
                };
            }
            else if (result == -4)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Coupon is drafted",
                    result = ""
                };
            }
            else if (result == -5)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -7,
                    errorMessage = "Valid date is expired",
                    result = ""
                };
            }
            else if (result == -6)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -2,
                    errorMessage = "Something happend while executing command",
                    result = ""
                };
            }

            return new BaseResponse<string>
            {
                isSucces = true,
                statusCode = 1,
                errorMessage = "",
                result = "Coupon voided"
            };
        }

        public async Task<BaseResponse<string>> ChangeStatusRequest(StatusCoupon coupon, string token)
        {
            int uid = _crypte.GetUserIdFromToken(token, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);


            var result = await _commandRepository.ChangeStatus(coupon, uid);

            if (result <= 0)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -2,
                    errorMessage = "Something happend while executing command",
                    result = ""
                };
            }

            return new BaseResponse<string>
            {
                isSucces = true,
                statusCode = 1,
                errorMessage = "",
                result = "Coupon " + coupon.operation + "ed"
            };
        }

       
    }
}
