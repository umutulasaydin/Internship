using CouponManagementServiceV2.Core.Model.Data;
using CouponManagementServiceV2.Core.Model.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementServiceV2.Core.Business.Interfaces
{
    public interface ICommandService
    {
        Task<BaseResponse<string>> SignUpRequest(Users item);
        Task<BaseResponse<CouponResponse>> CreateCouponRequest(Coupons coupon, string token);
        Task<BaseResponse<List<CouponResponse>>> CreateSerieCouponRequest(CouponSeries serie, string token);
        Task<BaseResponse<string>> RedeemCouponRequest(RedemptCoupon coupon, string token);
        Task<BaseResponse<string>> VoidCouponRequest(RedemptCoupon coupon, string token);
        Task<BaseResponse<string>> ChangeStatusRequest(StatusCoupon coupon, string token);
        Task<BaseResponse<string>> DeleteCouponRequest(GetCoupon<int> coupon);
    }
}
