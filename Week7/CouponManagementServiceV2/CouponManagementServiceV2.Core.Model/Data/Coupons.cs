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
        Expired = 5
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
        public double cpnCurrentRedemptValue { get; set; }
        public DateTime cpnInsTime { get; set; } = DateTime.Now;
        public DateTime cpnUpdTime { get; set; } = DateTime.Now;

        public Coupons()
        {
            cpnCurrentRedemptValue = cpnRedemptionLimit;
        }
    }

    public class CouponValidator : AbstractValidator<Coupons>
    {
        public CouponValidator()
        {
            RuleFor(x => x.cpnStatus).InclusiveBetween(1, 4);

            RuleFor(x => x.cpnStartDate).NotEmpty().NotNull().WithMessage("Start Date cannot be empty");

            RuleFor(x => x.cpnValidDate).NotEmpty().NotNull().WithMessage("Valid Date cannot be empty");

            RuleFor(x => x.ClientName).NotEmpty().NotNull().WithMessage("Client Name cannot be empty");

            RuleFor(x => x.ClientPos).NotEmpty().NotNull().WithMessage("Client Pos cannot be empty");
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
        public double cpnRedemptionLimit { get; set; }
        public double cpnCurrentRedemptValue { get; set; }
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

            RuleFor(x => x.ClientName).NotEmpty().NotNull().WithMessage("Client Name cannot be empty");

            RuleFor(x => x.ClientPos).NotEmpty().NotNull().WithMessage("Client Pos cannot be empty");
        }
    }
    
    public class StatusCoupon : BaseRequest
    {
        public int cpnId { get; set; }
        public int status { get; set; }
    }    

    public class StatusCouponValidator : AbstractValidator<StatusCoupon>
    {
        public StatusCouponValidator()
        {
            RuleFor(x => x.cpnId).NotEmpty().NotNull().WithMessage("Id cannot be empty");
            RuleFor(x => x.cpnId).GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.status).NotEmpty().NotNull().WithMessage("Status cannot be empty");
            RuleFor(x => x.status).InclusiveBetween(1, 4);

            RuleFor(x => x.ClientName).NotEmpty().NotNull().WithMessage("Client Name cannot be empty");

            RuleFor(x => x.ClientPos).NotEmpty().NotNull().WithMessage("Client Pos cannot be empty");

        }
    }

    public class GetCoupon<T> : BaseRequest
    {
        public T input { get; set; }
    }

    public class AllCouponReqeust : BaseRequest
    {
        public int pageNumber { get; set;}
        public int rowsOfPage { get; set; }
        public int cpnId { get; set;}
        public int couponStatus { get; set; } = 0;
        public string serieId { get; set; } = "";
        public string serieName { get; set; } = "";
        public string username { get; set; } = "";
        public string name { get; set; } = "";
        public DateTime startDateStart { get; set; } = new DateTime(1970, 1, 1);
        public DateTime startDateEnd { get; set; } = new DateTime(1970, 1, 1);
        public string startOrder { get; set; } = "";
        public DateTime validDateStart { get; set; } = new DateTime(1970, 1, 1);
        public DateTime validDateEnd { get; set; } = new DateTime(1970, 1, 1);
        public string validOrder { get; set; } = "";

    }

    public class AllCouponResponse : CouponResponse
    {
        public DateTime cpnInsTime { get; set; }
        public DateTime cpnUpdTime { get; set; }
        public string cpsSeriesId { get; set; }
        public string cpsSeriesName { get; set; }
        public int cpsCount { get; set; }
        public string cpsSeriesDesc { get; set; }
        public DateTime cpsInsTime { get; set; }
        public DateTime cpsUpdTime { get; set; }
        public string usUsername { get; set; }
        public string usName { get; set; }
        public string usPhoneNum { get; set; }
        public string usMail { get; set; }
        


    }

}
