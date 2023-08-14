using CouponManagementServiceV2.Core.Model.Shared;
using FluentValidation;

namespace CouponManagementServiceV2.Core.Model.Data
{
    public enum Status : int
    {
        Active = 1,
        Used = 2,
        Blocked = 3,
        Draft = 4,
    }
    public class Coupons : BaseRequest
    {
        public int cpnId { get; set; } = -1;
        public int cpnSerieId { get; set; } = -1;
        public int cpnCreatorId { get; set; } = -1;
        public string cpnCode { get; set; } = "";
        public int cpnStatus { get; set; } = (int)Status.Active;
        public DateTime cpnStartDate { get; set; } = DateTime.Now;
        public DateTime cpnValidDate { get; set; } = DateTime.Now;
        public double cpnRedemptionLimit { get; set; } = -1;
        public double cpnCurrentRedemptValue { get; set; } = -1;
        public DateTime cpnInsTime { get; set; } = DateTime.Now;
        public DateTime cpnUpdTime { get; set; } = DateTime.Now;
    }

    public class CouponValidator : AbstractValidator<Coupons>
    {
        public CouponValidator()
        {
            RuleFor(x => x.cpnStatus).InclusiveBetween(0, 3);

            RuleFor(x => x.cpnStartDate).NotEmpty().NotNull().WithMessage("Start Date cannot be empty");

            RuleFor(x => x.cpnValidDate).NotEmpty().NotNull().WithMessage("Valid Date cannot be empty");

        }
    }

    public class CouponResponse
    {
        public int cpnId { get; set; } = -1;
        public int cpnSerieId { get; set; } = -1;
        public int cpnCreatorId { get; set; } = -1;
        public string cpnCode { get; set; } = "";
        public int cpnStatus { get; set; } = (int)Status.Active;
        public DateTime cpnStartDate { get; set; } = DateTime.Now;
        public DateTime cpnValidDate { get; set; } = DateTime.Now;
        public double cpnRedemptionLimit { get; set; } = -1;
        public double cpnCurrentRedemptValue { get; set; } = -1;
    }

    public class RedemptCoupon : BaseRequest
    {
        public int cpnId { get; set; }
        public int amount { get; set; }
    }

    public class RedemptCouponValidator : AbstractValidator<RedemptCoupon>
    {
        public RedemptCouponValidator()
        {
            RuleFor(x => x.cpnId).NotEmpty().NotNull().WithMessage("Id cannot be empty");
            RuleFor(x => x.cpnId).GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.amount).NotEmpty().NotNull().WithMessage("Amount cannot be empty");
            RuleFor(x => x.amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
        }
    }
    
    public class StatusCoupon : BaseRequest
    {
        public int cpnId { get; set; }
        public string status { get; set; }
        public string operation { get; set; }
    }    

    public class StatusCouponValidator : AbstractValidator<StatusCoupon>
    {
        public StatusCouponValidator()
        {
            RuleFor(x => x.cpnId).NotEmpty().NotNull().WithMessage("Id cannot be empty");
            RuleFor(x => x.cpnId).GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.status).NotEmpty().NotNull().WithMessage("Status cannot be empty");

            RuleFor(x => x.operation).NotEmpty().NotNull().WithMessage("Operation cannot be empty");
        }
    }

   
}
