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
        Task<BaseResponse<Coupons>> CreateCouponRequest(Coupons coupon);
        Task<BaseResponse<Coupons>> CreateCouponRequest(Coupons coupon, string token, Status status);
    }
}
