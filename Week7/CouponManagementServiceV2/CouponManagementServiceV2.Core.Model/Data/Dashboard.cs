using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementServiceV2.Core.Model.Data
{
    public class series
    {
        public string cpsSeriesId { get; set; }
        public int cpsCount { get; set; }
    }
    public class Dashboard
    {
        public int TotalCoupon { get; set; }
        public int TotalSerie { get; set; }
        public int ValidCoupon { get; set; }
        public int Active { get; set; }
        public int Blocked { get; set; }
        public int Draft { get; set; }
        public int Used { get; set; }
        public int Expired { get; set; }
        public IEnumerable<series> series { get; set; } = null;
       
    }
}
