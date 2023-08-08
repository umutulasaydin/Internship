using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementServiceV2.Core.Model.Data
{
    enum Operation : int
    {
        Redeem,
        Void,
        Block,
        Unblock,
        Draft,
        Activate
    }
    public class CouponLog
    {
        public int cplId { get; set; }
        public int cplCouponId { get; set; }
        public int cplUserId { get; set; }
        public int cplOperation { get; set; }
        public DateTime cplInsTime { get; set; } = DateTime.Now;
        public string cplClientName { get; set; }
        public string cplClientPos { get; set; }
    }

    public class CouponLogValidator : AbstractValidator<CouponLog>
    {
        public CouponLogValidator()
        {
            RuleFor(x => x.cplId).NotEmpty().NotNull().WithMessage("Id cannot be empty");
            RuleFor(x => x.cplId).GreaterThan(0);

            RuleFor(x => x.cplCouponId).NotEmpty().NotNull().WithMessage("Coupon Id cannot be empty");

            RuleFor(x => x.cplUserId).NotEmpty().NotNull().WithMessage("User Id cannot be empty");

            RuleFor(x => x.cplOperation).NotEmpty().NotNull().WithMessage("Operation type cannot be empty");
            RuleFor(x => x.cplOperation).InclusiveBetween(0, 5);

            RuleFor(x => x.cplInsTime).NotEmpty().NotNull().WithMessage("Instant Date cannot be empty");

            RuleFor(x => x.cplClientName).NotEmpty().NotNull().WithMessage("Client Name cannot be empty");

            RuleFor(x => x.cplClientPos).NotEmpty().NotNull().WithMessage("Client Pos cannot be empty");

        }
    }
}
