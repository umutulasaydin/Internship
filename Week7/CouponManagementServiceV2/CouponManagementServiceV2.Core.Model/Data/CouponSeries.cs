using CouponManagementServiceV2.Core.Model.Shared;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementServiceV2.Core.Model.Data
{
    public class CouponSeries : BaseRequest
    {
        public int cpsId { get; set; } = -1;
        public int cpsUserId { get; set; }
        public string cpsSeriesId { get; set; }
        public string cpsSeriesName { get; set; }
        public string cpsSeriesDesc { get; set; }
        public int cpsCount { get; set; }
        public DateTime cpsInsTime { get; set; } = DateTime.Now;
        public DateTime cpsUpdTime { get; set; } = DateTime.Now;
        public int cpsStatus { get; set; } = (int)Status.Active;
        public DateTime cpsStartDate { get; set; } = DateTime.Now;
        public DateTime cpsValidDate { get; set; } = DateTime.Now;
        public double cpsRedemptionLimit { get; set; } = -1;
        public double cpsCurrentRedemptValue { get; set; }

        public CouponSeries()
        {
            cpsCurrentRedemptValue = cpsRedemptionLimit;
        }
    }

    public class CouponSeriesValidator : AbstractValidator<CouponSeries>
    {
        public CouponSeriesValidator() 
        {
     

            RuleFor(x => x.cpsSeriesId).NotEmpty().NotNull().WithMessage("Series Id cannot be empty");

            RuleFor(x => x.cpsSeriesName).NotEmpty().NotNull().WithMessage("Series Name cannot be empty");

            RuleFor(x => x.ClientName).NotEmpty().NotNull().WithMessage("Client Name cannot be empty");

            RuleFor(x => x.ClientPos).NotEmpty().NotNull().WithMessage("Client Pos cannot be empty");
        }
    }


    public class AllSeriesReqeust : BaseRequest
    {
        public int pageNumber { get; set; }
        public int rowsOfPage { get; set; }
        
        public int id { get; set; } = 0;
        public string serieId { get; set; } = "";
        public DateTime insTimeStart { get; set; } = new DateTime(1970, 1, 1);
        public DateTime insTimeEnd { get; set; } = new DateTime(1970, 1, 1);
        public string insOrder { get; set; } = "";

    }



}
