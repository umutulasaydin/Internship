using CouponManagementServiceV2.Core.Model.Data;
using CouponManagementServiceV2.Core.Model.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementServiceV2.Core.Business.Interfaces
{
    public interface IQueryService
    {
        Task<BaseResponse<string>> LoginRequest(Login item);
        Task<BaseResponse<CouponResponse>> GetCouponByIdRequest(int id);
        Task<BaseResponse<IEnumerable<CouponResponse>>> GetCouponsBySerieIdRequest(string id);
        Task<BaseResponse<IEnumerable<CouponResponse>>> GetCouponsByUsernameRequest(string username);

    }
}
