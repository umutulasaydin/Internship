using CouponManagementServiceV2.Core.Data.Interfaces;
using CouponManagementServiceV2.Core.Model.Data;
using CouponManagementServiceV2.Core.Model.Shared;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponManagementServiceV2.Core.Data.Repo
{
    public class QueryRepository : IQueryRepository
    {
        private readonly DatabaseConfig _dbConnect;
        private readonly Cryption _crypte;

        public QueryRepository(DatabaseConfig dbConnect)
        {
            _dbConnect = dbConnect;
            _crypte = new Cryption();
        }

        public async Task<Users> LoginOperation(Login item)
        {
            
            try
            {
                var query = "SELECT * FROM Users WHERE usUsername = @username AND usPassword = @password" ;
                using var connection = _dbConnect.Connect("Query");
                string pass = _crypte.hash(item.Password + _crypte._saltkey);
                return  await connection.QueryFirstAsync<Users>(query, new { username = item.Username, password = pass });
                
            }
            catch
            {
                return null;
            }

           
        }

        public async Task<CouponResponse> GetCouponById(int id)
        {
            try
            {
                var query = "SELECT * FROM Coupons WHERE cpnId = @id";
                using var connection = _dbConnect.Connect("Query");
                return await connection.QueryFirstAsync<CouponResponse>(query, new { id = id });
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<CouponResponse>> GetCouponsBySerieId(string id)
        {
            try
            {
                var query = "SELECT cpnId, cpnSerieId, cpnCreatorId, cpnCode, cpnStartDate, cpnValidDate, cpnRedemptionLimit, cpnCurrentRedemptValue FROM Coupons INNER JOIN CouponSeries ON (cpnSerieId = cpsId) WHERE cpsSeriesId = @id";
                using var connection = _dbConnect.Connect("Query");
                return await connection.QueryAsync<CouponResponse>(query, new { id = id });
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<CouponResponse>> GetCouponsByUsername(string username)
        {
            try
            {
                var query = "SELECT cpnId, cpnSerieId, cpnCreatorId, cpnCode, cpnStartDate, cpnValidDate, cpnRedemptionLimit, cpnCurrentRedemptValue FROM Coupons INNER JOIN Users ON (cpnCreatorId = usId) WHERE usUsername = @username";
                using var connection = _dbConnect.Connect("Query");
                return await connection.QueryAsync<CouponResponse>(query, new { username = username });
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<CouponLogResponse>> GetCouponInfo(int id)
        {
            try
            {
                var query = "SELECT * FROM CouponLog WHERE cplCouponId = @id";
                using var connection = _dbConnect.Connect("Query");
                return await connection.QueryAsync<CouponLogResponse>(query, new { id = id });
            }
            catch
            {
                return null;
            }
            
        }

        public async Task<IEnumerable<CouponLogResponse>> GetCouponInfoByUserId(int id)
        {
            try
            {
                var query = "SELECT * FROM CouponLog WHERE cplUserId = @id";
                using var connection = _dbConnect.Connect("Query");
                return await connection.QueryAsync<CouponLogResponse>(query, new { id = id});
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<CouponResponse>> GetValidCoupons()
        {
            try
            {
                var query = "SELECT * FROM Coupons WHERE cpnStartDate < CURRENT_TIMESTAMP AND cpnValidDate > CURRENT_TIMESTAMP";
                using var connection = _dbConnect.Connect("Query");
                return await connection.QueryAsync<CouponResponse>(query);
            }
            catch
            {
                return null;
            }
        }
    }
}
