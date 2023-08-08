using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementServiceV2.Core.Model.Data
{
    public enum Status : int
    {
        Active,
        Used,
        Blocked,
        Draft
    }
    public class Coupons
    {
        public int cpnId { get; set; } = -1;
        public int cpnSerieId { get; set; } = -1;
        public int cpnCreatorId { get; set; } = -1;
        public string cpnCode { get; set; } = null;
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
            RuleFor(x => x.cpnId).NotEmpty().NotNull().WithMessage("Id cannot be empty");
            RuleFor(x => x.cpnId).GreaterThan(0);

            RuleFor(x => x.cpnCreatorId).NotEmpty().NotNull().WithMessage("Creator Id cannot be empty");

            RuleFor(x => x.cpnCode).NotEmpty().NotNull().WithMessage("Coupon Code cannot be empty");

            RuleFor(x => x.cpnStatus).NotEmpty().NotNull().WithMessage("Status cannot be empty");
            RuleFor(x => x.cpnStatus).InclusiveBetween(0, 3);

            RuleFor(x => x.cpnStartDate).NotEmpty().NotNull().WithMessage("Start Date cannot be empty");

            RuleFor(x => x.cpnValidDate).NotEmpty().NotNull().WithMessage("Valid Date cannot be empty");

            RuleFor(x => x.cpnRedemptionLimit).NotEmpty().NotNull().WithMessage("Redemption Limit cannot be empty");

            RuleFor(x => x.cpnCurrentRedemptValue).NotEmpty().NotNull().WithMessage("Current Redemption Value cannot be empty");

            RuleFor(x => x.cpnInsTime).NotEmpty().NotNull().WithMessage("Instant Date cannot be empty");

            RuleFor(x => x.cpnUpdTime).NotEmpty().NotNull().WithMessage("Update Date cannot be empty");

        }
    }
}
