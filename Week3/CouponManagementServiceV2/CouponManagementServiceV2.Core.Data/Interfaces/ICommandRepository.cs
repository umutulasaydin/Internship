using CouponManagementServiceV2.Core.Model.Data;


namespace CouponManagementServiceV2.Core.Data.Interfaces
{
    public interface ICommandRepository
    {
        Task<int> SignUpOperation(Users item);
        Task<int> CreateCoupon(Coupons coupon);
    }
}
