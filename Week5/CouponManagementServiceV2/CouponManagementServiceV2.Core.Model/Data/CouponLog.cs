using CouponManagementServiceV2.Core.Model.Shared;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementServiceV2.Core.Model.Data
{
    public enum Operation : int
    {
        Redeem = 1,
        Void = 2,
        Block = 3,
        Draft = 4,
        Activate = 5,
        Used = 6
    }
    public class CouponLog : BaseRequest
    {
        public int cplId { get; set; } = -1;
        public int cplCouponId { get; set; }
        public int cplUserId { get; set; }
        public int cplOperation { get; set; }
        public DateTime cplInsTime { get; set; } = DateTime.Now;

    }

    public class CouponLogValidator : AbstractValidator<CouponLog>
    {
        public CouponLogValidator()
        {
           

            RuleFor(x => x.cplCouponId).NotEmpty().NotNull().WithMessage("Coupon Id cannot be empty");

            RuleFor(x => x.cplUserId).NotEmpty().NotNull().WithMessage("User Id cannot be empty");

            RuleFor(x => x.cplOperation).NotEmpty().NotNull().WithMessage("Operation type cannot be empty");
            RuleFor(x => x.cplOperation).InclusiveBetween(0, 5);

            RuleFor(x => x.cplInsTime).NotEmpty().NotNull().WithMessage("Instant Date cannot be empty");

            RuleFor(x => x.ClientName).NotEmpty().NotNull().WithMessage("Client Name cannot be empty");

            RuleFor(x => x.ClientPos).NotEmpty().NotNull().WithMessage("Client Pos cannot be empty");

        }
    }

    public class CouponLogResponse
    {
        public int cplId { get; set; } = -1;
        public int cplCouponId { get; set; }
        public int cplUserId { get; set; }
        public int cplOperation { get; set; }
        public DateTime cplInsTime { get; set; } = DateTime.Now;
        public string cplClientName { get; set; }
        public string cplClientPos { get; set;}
    }
}
