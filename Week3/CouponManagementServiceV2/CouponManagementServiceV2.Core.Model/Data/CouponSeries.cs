using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementServiceV2.Core.Model.Data
{
    public class CouponSeries
    {
        public int cpsId { get; set; }
        public string cpsSeriesId { get; set; }
        public string cpsSerieName { get; set; }
        public string cpsSerieDesc { get; set; }
        public DateTime cpsInsTime { get; set; } = DateTime.Now;
        public DateTime cpsUpdTime { get; set; } = DateTime.Now;
    }

    public class CouponSeriesValidator : AbstractValidator<CouponSeries>
    {
        public CouponSeriesValidator() 
        {
            RuleFor(x => x.cpsId).NotEmpty().NotNull().WithMessage("Id cannot be empty");
            RuleFor(x => x.cpsId).GreaterThan(0);

            RuleFor(x => x.cpsSeriesId).NotEmpty().NotNull().WithMessage("Series Id cannot be empty");

            RuleFor(x => x.cpsSerieName).NotEmpty().NotNull().WithMessage("Series Name cannot be empty");

            
        }
    }
}
