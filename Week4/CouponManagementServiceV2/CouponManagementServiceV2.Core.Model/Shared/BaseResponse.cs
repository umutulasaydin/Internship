using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementServiceV2.Core.Model.Shared
{
    public class BaseResponse<T>
    {
        public T result { get; set; }
        public bool isSucces { get; set; }
        public int statusCode { get; set; }
        public string errorMessage { get; set; }
    }
}
