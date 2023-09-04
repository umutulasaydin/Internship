using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementServiceV2.Core.Model.Data
{
    public class PageInfo<T>
    {
        public T data { get; set; }
        public int maxPage { get; set; }
    }
}
