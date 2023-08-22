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
        public string cpsSerieName { get; set; }
        public string cpsSerieDesc { get; set; }
        public int cpsSerieCount { get; set; }
        public DateTime cpsInsTime { get; set; } = DateTime.Now;
        public DateTime cpsUpdTime { get; set; } = DateTime.Now;
        public int cpsStatus { get; set; } = (int)Status.Active;
        public DateTime cpsStartDate { get; set; } = DateTime.Now;
        public DateTime cpsValidDate { get; set; } = DateTime.Now;
        public double cpsRedemptionLimit { get; set; } = -1;
        public double cpsCurrentRedemptValue { get; set; } = -1;
    }

    public class CouponSeriesValidator : AbstractValidator<CouponSeries>
    {
        public CouponSeriesValidator() 
        {
     

            RuleFor(x => x.cpsSeriesId).NotEmpty().NotNull().WithMessage("Series Id cannot be empty");

            RuleFor(x => x.cpsSerieName).NotEmpty().NotNull().WithMessage("Series Name cannot be empty");

            RuleFor(x => x.ClientName).NotEmpty().NotNull().WithMessage("Client Name cannot be empty");

            RuleFor(x => x.ClientPos).NotEmpty().NotNull().WithMessage("Client Pos cannot be empty");
        }
    }




}
