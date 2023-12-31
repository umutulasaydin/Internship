﻿using CouponManagementServiceV2.Core.Business.Interfaces;
using CouponManagementServiceV2.Core.Data.Interfaces;
using CouponManagementServiceV2.Core.Model.Data;
using CouponManagementServiceV2.Core.Model.Shared;
using CouponManagementServiceV2.Core.Model.Shared.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using NLog;

namespace CouponManagementServiceV2.Core.Business.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandRepository _commandRepository;
        private readonly Cryption _crypte;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<Error> _errorLocalizer;
        private readonly IStringLocalizer<ErrorCode> _errorCodeLocalizer;
        private readonly Logger _logger;


        public CommandService(ICommandRepository commandRepository, IConfiguration configuration, IStringLocalizer<ErrorCode> errorCodeLocalizer, IStringLocalizer<Error> errorLocalizer)
        {
            _commandRepository = commandRepository;
            _crypte = new Cryption();
            _configuration = configuration;
            _errorLocalizer = errorLocalizer;
            _errorCodeLocalizer = errorCodeLocalizer;
            _logger = LogManager.GetCurrentClassLogger();
        }
        public async Task<BaseResponse<string>> SignUpRequest(Users item)
        {
            var result = await _commandRepository.SignUpOperation(item);
            if (result == 0 || result == -1)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["USER_EXIST"],
                    errorMessage = _errorLocalizer["USER_EXIST"],
                    result = ""
                };
            }
            else if (result == -2)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["PROCESS_FAIL"],
                    errorMessage = _errorLocalizer["PROCESS_FAIL"],
                    result = ""
                };
            }

            return new BaseResponse<string>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
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
                    statusCode = _errorCodeLocalizer["PROCESS_FAIL"],
                    errorMessage = _errorLocalizer["PROCESS_FAIL"],
                    result = null
                };
            }
            CouponResponse response = new CouponResponse
            {
                cpnCode = coupon.cpnCode,
                cpnCreatorId = coupon.cpnCreatorId,
                cpnCurrentRedemptValue = coupon.cpnRedemptionLimit,
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
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = response
            };
        }

        public async Task<BaseResponse<string>> CreateSerieCouponRequest(CouponSeries serie, string token)
        {
            serie.cpsUserId = _crypte.GetUserIdFromToken(token, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);

            var result = await _commandRepository.CreateSeriesCoupon(serie);

            if (result == null)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["PROCESS_FAIL"],
                    errorMessage = _errorLocalizer["PROCESS_FAIL"],
                    result = null
                };
            }

            return new BaseResponse<string>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = _errorLocalizer["SERIE_SUCCESS"]
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
                    statusCode = _errorCodeLocalizer["NO_LIMIT"],
                    errorMessage = _errorLocalizer["NO_LIMIT"],
                    result = ""
                };
            }
            else if (result == -2)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["USED_COUPON"],
                    errorMessage = _errorLocalizer["USED_COUPON"],
                    result = ""
                };
            }
            else if (result == -3)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["BLOCK_COUPON"],
                    errorMessage = _errorLocalizer["BLOCK_COUPON"],
                    result = ""
                };
            }
            else if (result == -4)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["DRAFT_COUPON"],
                    errorMessage = _errorLocalizer["DRAFT_COUPON"],
                    result = ""
                };
            }
            else if (result == -5)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["DATE_EXPIRED"],
                    errorMessage = _errorLocalizer["DATE_EXPIRED"],
                    result = ""
                };
            }
            else if (result == -6)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["PROCESS_FAIL"],
                    errorMessage = _errorLocalizer["PROCESS_FAIL"],
                    result = ""
                };
            }

            return new BaseResponse<string>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = _errorLocalizer["REDEEM_CPN"]
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
                    statusCode = _errorCodeLocalizer["NO_LIMIT"],
                    errorMessage = _errorLocalizer["NO_LIMIT"],
                    result = ""
                };
            }
            else if (result == -2)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["USED_COUPON"],
                    errorMessage = _errorLocalizer["USED_COUPON"],
                    result = ""
                };
            }
            else if (result == -3)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["BLOCK_COUPON"],
                    errorMessage = _errorLocalizer["BLOCK_COUPON"],
                    result = ""
                };
            }
            else if (result == -4)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["DRAFT_COUPON"],
                    errorMessage = _errorCodeLocalizer["DRAFT_COUPON"],
                    result = ""
                };
            }
            else if (result == -5)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["DATE_EXPIRED"],
                    errorMessage = _errorLocalizer["DATE_EXPIRED"],
                    result = ""
                };
            }
            else if (result == -6)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["PROCESS_FAIL"],
                    errorMessage = _errorLocalizer["PROCESS_FAIL"],
                    result = ""
                };
            }

            return new BaseResponse<string>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = _errorLocalizer["VOID_CPN"]
            };
        }

        public async Task<BaseResponse<string>> ChangeStatusRequest(StatusCoupon coupon, string token)
        {
            int uid = _crypte.GetUserIdFromToken(token, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);


            var result = await _commandRepository.ChangeStatus(coupon, uid);

            if (result == 1)
            {
                if ((Status)coupon.status == Status.Active)
                {
                    return new BaseResponse<string>
                    {
                        isSucces = true,
                        statusCode = _errorCodeLocalizer["SUCCESS"],
                        errorMessage = _errorLocalizer["SUCCESS"],
                        result = _errorLocalizer["ACTIVATE"]
                    };
                }
                return new BaseResponse<string>
                {
                    isSucces = true,
                    statusCode = _errorCodeLocalizer["SUCCESS"],
                    errorMessage = _errorLocalizer["SUCCESS"],
                    result = _errorLocalizer["BLOCKED"]
                };
            }

            else if (result == -1)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_ACTIVATE"],
                    errorMessage = _errorLocalizer["NO_ACTIVATE"],
                    result = ""
                };
            }

            else if (result == -2)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_USED"],
                    errorMessage = _errorLocalizer["NO_USED"],
                    result = ""
                };
            }

            else if (result == -3)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_BLOCK"],
                    errorMessage = _errorLocalizer["NO_BLOCK"],
                    result = ""
                };
            }

            else if (result == -4)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_DRAFT"],
                    errorMessage = _errorLocalizer["NO_DRAFT"],
                    result = ""
                };
            }

            else 
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["PROCESS_FAIL"],
                    errorMessage = _errorLocalizer["PROCESS_FAIL"],
                    result = ""
                };
            }

            
        }
        
        public async Task<BaseResponse<string>> DeleteCouponRequest(GetCoupon<int> coupon)
        {

            var result = await _commandRepository.DeleteCoupon(coupon);

            if (result == 0 || result == -1)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_COUPON_ID"],
                    errorMessage = _errorLocalizer["NO_COUPON_ID"],
                    result = ""
                };
            }

            else if (result == -2)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["PROCESS_FAIL"],
                    errorMessage = _errorLocalizer["PROCESS_FAIL"],
                    result = ""
                };
            }

            return new BaseResponse<string>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = _errorLocalizer["DELETE"]
            };
        }

        public async Task<BaseResponse<string>> DeleteSerieRequest(GetCoupon<int> serie)
        {

            var result = await _commandRepository.DeleteSerie(serie);

            if (result == 0 || result == -1)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["NO_SERIE_ID"],
                    errorMessage = _errorLocalizer["NO_SERIE_ID"],
                    result = ""
                };
            }

            else if (result == -2)
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = _errorCodeLocalizer["PROCESS_FAIL"],
                    errorMessage = _errorLocalizer["PROCESS_FAIL"],
                    result = ""
                };
            }

            return new BaseResponse<string>
            {
                isSucces = true,
                statusCode = _errorCodeLocalizer["SUCCESS"],
                errorMessage = _errorLocalizer["SUCCESS"],
                result = _errorLocalizer["DELETE"]
            };
        }

        public async Task CheckCouponRequest()
        {
            var result = await _commandRepository.CheckCoupons();

            if (result == -2)
            {
                _logger.Error("Error Check Coupon didn't work well");
            }
            else if (result == -1)
            {
                _logger.Info("There is no coupon changing expired");
            }
            else if (result >= 0)
            {
                _logger.Info((result/2).ToString()+" coupons status changed to expired");
            }
        }





    }
}
