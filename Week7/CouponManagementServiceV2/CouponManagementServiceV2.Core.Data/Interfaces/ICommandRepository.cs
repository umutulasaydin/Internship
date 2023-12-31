﻿using CouponManagementServiceV2.Core.Model.Data;


namespace CouponManagementServiceV2.Core.Data.Interfaces
{
    public interface ICommandRepository
    {
        Task<int> SignUpOperation(Users item);
        Task<int> CreateCoupon(Coupons coupon);
        Task<int> CreateSeriesCoupon(CouponSeries serie);
        Task<int> RedeemCoupon(RedemptCoupon coupon, int uid);
        Task<int> VoidCoupon(RedemptCoupon coupon, int uid);
        Task<int> ChangeStatus(StatusCoupon coupon, int uid);
        Task<int> DeleteCoupon(GetCoupon<int> coupon);
        Task<int> DeleteSerie(GetCoupon<int> serie);
        Task<int> CheckCoupons();
    }
}
