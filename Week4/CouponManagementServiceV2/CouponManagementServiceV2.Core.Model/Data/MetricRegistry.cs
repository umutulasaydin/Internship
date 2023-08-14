

using App.Metrics;
using App.Metrics.Counter;

namespace CouponManagementServiceV2.Core.Model.Data
{
    public class MetricRegistry
    {
        public static CounterOptions CreatedUserCounter => new CounterOptions
        {
            Name = "Created User",
            Context = "AuthApi",
            MeasurementUnit = Unit.Calls
        };

        public static CounterOptions LoginCounter => new CounterOptions
        {
            Name = "User Login",
            Context = "AuthApi",
            MeasurementUnit = Unit.Calls
        };

        public static CounterOptions CreatedCuponCounter => new CounterOptions
        {
            Name = "Created Coupon",
            Context = "CouponApi",
            MeasurementUnit = Unit.Calls
        };

        public static CounterOptions CreatedSerieCounter => new CounterOptions
        {
            Name = "Created Serie",
            Context = "CouponApi",
            MeasurementUnit = Unit.Calls
        };

        public static CounterOptions CreatedRedeemCounter => new CounterOptions
        {
            Name = "Created Redeem",
            Context = "CouponApi",
            MeasurementUnit = Unit.Calls
        };

        public static CounterOptions CreatedVoidCounter => new CounterOptions
        {
            Name = "Created Void",
            Context = "CouponApi",
            MeasurementUnit = Unit.Calls
        };

        public static CounterOptions CreateStatusChangeCounter => new CounterOptions
        {
            Name = "Created Status Change",
            Context = "CouponApi",
            MeasurementUnit = Unit.Calls
        };

        public static CounterOptions GetCouponCounter => new CounterOptions
        {
            Name = "Get Coupon",
            Context = "CouponApi",
            MeasurementUnit = Unit.Calls
        };

        public static CounterOptions GetSerieCounter => new CounterOptions
        {
            Name = "Get Serie",
            Context = "CouponApi",
            MeasurementUnit = Unit.Calls
        };

        public static CounterOptions GetCouponByUsernameCounter => new CounterOptions
        {
            Name = "Get Coupon By Username",
            Context = "CouponApi",
            MeasurementUnit = Unit.Calls
        };
    }
}
