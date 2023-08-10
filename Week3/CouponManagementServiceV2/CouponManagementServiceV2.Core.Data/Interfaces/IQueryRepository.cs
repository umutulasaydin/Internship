using CouponManagementServiceV2.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementServiceV2.Core.Data.Interfaces
{
    public interface IQueryRepository
    {
        Task<Users> LoginOperation(Login item);
        Task<CouponResponse> GetCouponById(int id);
        Task<IEnumerable<CouponResponse>> GetCouponsBySerieId(string id);
        Task<IEnumerable<CouponResponse>> GetCouponsByUsername(string username);
    }
}
