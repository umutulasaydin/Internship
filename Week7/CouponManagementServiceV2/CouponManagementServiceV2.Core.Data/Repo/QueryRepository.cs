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

        public async Task<PageInfo<IEnumerable<AllCouponResponse>>> GetAllCoupons(AllCouponReqeust parameters)
        {
            try
            {
                var query = "EXEC dbo.GetCouponsFilterSort @PageNumber, @RowsOfPage, @couponStatus, @serieId, @serieName, @username, @name, @startDateStart, @startDateEnd, @startOrder, @validDateStart, @validDateEnd, @validOrder";
                var query2 = "EXEC dbo.GetCouponLength @couponStatus, @serieId, @serieName, @username, @name, @startDateStart, @startDateEnd, @validDateStart, @validDateEnd";
                using var connection = _dbConnect.Connect("Query");
                var data = await connection.QueryAsync<AllCouponResponse>(query, new { PageNumber = parameters.pageNumber, RowsOfPage = parameters.rowsOfPage, couponStatus = parameters.couponStatus, serieId = parameters.serieId, serieName = parameters.serieName, username = parameters.username, name = parameters.name, startDateStart = parameters.startDateStart, startDateEnd = parameters.startDateEnd, startOrder = parameters.startOrder, validDateStart = parameters.validDateStart, validDateEnd = parameters.validDateEnd, validOrder = parameters.validOrder });
                var maxPage = await connection.QueryAsync<int>(query2, new { couponStatus = parameters.couponStatus, serieId = parameters.serieId, serieName = parameters.serieName, username = parameters.username, name = parameters.name, startDateStart = parameters.startDateStart, startDateEnd = parameters.startDateEnd, validDateStart = parameters.validDateStart, validDateEnd = parameters.validDateEnd });
                return new PageInfo<IEnumerable<AllCouponResponse>>
                {
                    data = data,
                    maxPage = maxPage.ToList()[0]
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<PageInfo<IEnumerable<CouponSeries>>> GetAllSeries(AllSeriesReqeust parameters)
        {
            try
            {
                var query = "EXEC dbo.GetSeriesFilter @PageNumber, @RowsOfPage, @id, @serieId, @insStart, @insEnd, @insOrder";
                var query2 = "EXEC dbo.GetSeriesLength @id, @serieId, @insStart, @insEnd, @insOrder";
                using var connection = _dbConnect.Connect("Query");
                var data = await connection.QueryAsync<CouponSeries>(query, new { PageNumber = parameters.pageNumber, RowsOfPage = parameters.rowsOfPage, id = parameters.id, serieId = parameters.serieId, insStart = parameters.insTimeStart, insEnd = parameters.insTimeEnd, insOrder = parameters.insOrder });
                var maxPage = await connection.QueryAsync<int>(query2, new {id = parameters.id, serieId = parameters.serieId, insStart = parameters.insTimeStart, insEnd = parameters.insTimeEnd, insOrder = parameters.insOrder });
                return new PageInfo<IEnumerable<CouponSeries>>
                {
                    data = data,
                    maxPage = maxPage.ToList()[0]
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<PageInfo<IEnumerable<AllCouponLogResponse>>> GetAllCouponLogs(AllCouponLogRequest parameters)
        {
            try
            {
                var query = "EXEC dbo.GetLogsFilterSort @PageNumber, @RowsOfPage, @couponId, @operation, @username, @name, @startDate, @endDate, @dateOrder, @clientName, @clientPos";
                using var connection = _dbConnect.Connect("Query");
                var data = await connection.QueryAsync<AllCouponLogResponse>(query, new { PageNumber = parameters.pageNumber, RowsOfPage = parameters.rowsOfPage, couponId = parameters.couponId, operation = parameters.operation, username = parameters.username, name = parameters.name, startDate = parameters.startDate, endDate = parameters.endDate, dateOrder = parameters.dateOrder, clientName = parameters.ClientName, clientPos = parameters.ClientPos });
                var maxPage = await connection.QueryAsync<int>("SELECT (COUNT(*) FROM CouponLog");
                return new PageInfo<IEnumerable<AllCouponLogResponse>>
                {
                    data = await connection.QueryAsync<AllCouponLogResponse>(query, new { PageNumber = parameters.pageNumber, RowsOfPage = parameters.rowsOfPage, couponId = parameters.couponId, operation = parameters.operation, username = parameters.username, name = parameters.name, startDate = parameters.startDate, endDate = parameters.endDate, dateOrder = parameters.dateOrder, clientName = parameters.ClientName, clientPos = parameters.ClientPos }),
                    maxPage = maxPage.ToList()[0]
                };                
            }
            catch
            {
                return null;
            }
        }

        public async Task<Users> GetUserInfo(int id)
        {
            try
            {
                var query = "SELECT usId, usUsername, usName, usMail, usPhoneNum FROM Users WHERE usId = @id";
                using var connection = _dbConnect.Connect("Query");
                var user = await connection.QueryAsync<Users>(query, new { id = id });
            
                return user.First();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Dashboard>Dashboard()
        {
            try
            {
                var query = "EXEC dbo.Dashboard @TotalCoupon, @TotalSerie, @ValidCoupon, @Active, @Blocked, @Draft, @Used, @Expired";
                var query2 = "SELECT cpsSeriesId, cpsCount FROM CouponSeries";
                using var connection = _dbConnect.Connect("Query");
                
                var dashboard = await connection.QueryAsync<Dashboard>(query, new { TotalCoupon = 0, TotalSerie = 0, ValidCoupon = 0, Active = 0, Blocked = 0, Draft = 0, Used = 0, Expired=0});
                var series = await connection.QueryAsync<series>(query2);
                dashboard.First().series = series;
                return dashboard.First();
            }
            catch
            {
                return null;
            }
        }

    }
}
