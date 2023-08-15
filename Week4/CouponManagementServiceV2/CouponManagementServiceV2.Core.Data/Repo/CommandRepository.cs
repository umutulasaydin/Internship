using CouponManagementServiceV2.Core.Data.Interfaces;
using CouponManagementServiceV2.Core.Model.Shared;
using CouponManagementServiceV2.Core.Model.Data;
using Dapper;


namespace CouponManagementServiceV2.Core.Data.Repo
{
    public class CommandRepository : ICommandRepository
    {
        private readonly DatabaseConfig _dbConnect;
        private readonly Cryption _crypte;
       
        public CommandRepository(DatabaseConfig dbConnect)
        {
            _dbConnect = dbConnect;
            _crypte = new Cryption();
        }
        public async Task<int> SignUpOperation(Users item)
        {
            int result;
            try
            {
                var query = "IF NOT EXISTS (SELECT * FROM Users WHERE usUsername = @username) INSERT INTO Users (usUsername,usPassword,usName,usMail,usPhoneNum) Values (@username,@password,@name,@email,@phone)";
                using var connection = _dbConnect.Connect("Command");
                string pass = _crypte.hash(item.usPassword + _crypte._saltkey);
                result = await connection.ExecuteAsync(query, new { username = item.usUsername, password = pass, name = item.usName, email = item.usMail, phone = item.usPhoneNum });
            }
            catch
            {
                result = -2;
            }
            
            return result;
        }

        public async Task<int> CreateCoupon(Coupons coupon)
        {
            int result;
            coupon.cpnCode = Guid.NewGuid().ToString();

            var query = "INSERT INTO Coupons (cpnSerieId, cpnCreatorId, cpnCode, cpnStatus, cpnStartDate, cpnValidDate, cpnRedemptionLimit, cpnCurrentRedemptValue, cpnInsTime, cpnUpdTime) Values (@serieId, @creatorId, @code, @status, @start, @valid, @redemptLimit, @currentLimit, @ins, @upd);SELECT @@IDENTITY";
            var query2 = "INSERT INTO CouponLog (cplCouponId, cplUserId, cplOperation, cplClientName, cplClientPos) VALUES (@@IDENTITY, @uid, @op, @cname, @clpos)";

            using var connection = _dbConnect.Connect("Command");
            connection.Open();
            
           
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    
                    result = await connection.QueryFirstAsync<int>(query, new { serieId = coupon.cpnSerieId, CreatorId = coupon.cpnCreatorId, code = coupon.cpnCode, status = coupon.cpnStatus, start = coupon.cpnStartDate, valid = coupon.cpnValidDate, redemptLimit = coupon.cpnRedemptionLimit, currentLimit = coupon.cpnCurrentRedemptValue, ins = coupon.cpnInsTime, upd = coupon.cpnUpdTime }, transaction);
 
                    await connection.ExecuteAsync(query2, new { uid = coupon.cpnCreatorId, op = (int)Operation.Activate, cname = coupon.ClientName, clpos = coupon.ClientPos }, transaction);
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    return -1;
                }
            }
            
            
            
        }

        public async Task<List<CouponResponse>> CreateSeriesCoupon(CouponSeries series)
        {
            int result = 0;
            List<CouponResponse> coupons = new List<CouponResponse>();
            var query1 = "DECLARE @id INT;EXEC dbo.GetSeriesId @serieId, @serieName, @serieDesc, @id OUTPUT;SELECT @id";
            var query2 = "INSERT INTO Coupons (cpnSerieId, cpnCreatorId, cpnCode, cpnStatus, cpnStartDate, cpnValidDate, cpnRedemptionLimit, cpnCurrentRedemptValue, cpnInsTime, cpnUpdTime) Values (@serieId, @creatorId, @code, @status, @start, @valid, @redemptLimit, @currentLimit, @ins, @upd);SELECT @@IDENTITY";
            var query3 = "INSERT INTO CouponLog (cplCouponId, cplUserId, cplOperation, cplClientName, cplClientPos) VALUES (@@IDENTITY, @uid, @op, @cname, @clpos)";
            using var connection = _dbConnect.Connect("Command");
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var cpnId = 0;
                    var cpsId = await connection.ExecuteAsync(query1, new { serieId = series.cpsSeriesId, serieName = series.cpsSerieName, serieDesc = series.cpsSerieDesc }, transaction);
                    

                    for (int i = 0; i< series.cpsSerieCount; i++)
                    {
                        string code = Guid.NewGuid().ToString();
                        
                        cpnId = await connection.QueryFirstAsync<int>(query2, new { serieId = cpsId, CreatorId = series.cpsUserId, code = code, status = series.cpsStatus, start = series.cpsStartDate, valid = series.cpsValidDate, redemptLimit = series.cpsRedemptionLimit, currentLimit = series.cpsCurrentRedemptValue, ins = series.cpsInsTime, upd = series.cpsUpdTime }, transaction);
                        
                      
                        result = await connection.ExecuteAsync(query3, new { uid = series.cpsUserId, op = (int) Operation.Activate, cname = series.ClientName, clpos = series.ClientPos }, transaction);
                        if (result <= 0)
                        {
                            throw new Exception(); ;
                        }
                        
                        coupons.Add(new CouponResponse()
                        {
                            cpnId = cpnId,
                            cpnSerieId = cpsId,
                            cpnCreatorId = series.cpsUserId,
                            cpnCode = code,
                            cpnStatus = series.cpsStatus,
                            cpnStartDate = series.cpsStartDate,
                            cpnValidDate = series.cpsValidDate,
                            cpnRedemptionLimit = series.cpsRedemptionLimit,
                            cpnCurrentRedemptValue = series.cpsCurrentRedemptValue,
                        });
                    }
                    transaction.Commit();
                    return coupons;

                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<int> RedeemCoupon(RedemptCoupon coupon, int uid)
        {
            int result;
            var query = "DECLARE @result INT;EXEC dbo.Redemption @couponid, @amount, @result OUTPUT;SELECT @result";
            var query2 = "INSERT INTO CouponLog VALUES (@cid, @uid, @op, @time, @cname, @cpos)";

            using var connection = _dbConnect.Connect("Command");
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    result = await connection.QueryFirstAsync<int>(query, new { couponid = coupon.cpnId, amount = coupon.amount}, transaction);
                    if (result <= 0)
                    {
                        transaction.Rollback();
                        return result;
                    }
                    await connection.ExecuteAsync(query2, new { cid = coupon.cpnId, uid = uid, op = (int) Operation.Redeem, time = DateTime.Now, cname = coupon.ClientName, cpos = coupon.ClientPos }, transaction);

                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    return -6;
                }
            }

        }

        public async Task<int> VoidCoupon(RedemptCoupon coupon, int uid)
        {
            int result;
            var query = "DECLARE @result INT;EXEC dbo.Void @couponid, @amount, @result OUTPUT;SELECT @result";
            var query2 = "INSERT INTO CouponLog VALUES (@cid, @uid, @op, @time, @cname, @cpos)";

            using var connection = _dbConnect.Connect("Command");
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    result = await connection.QueryFirstAsync<int>(query, new { couponid = coupon.cpnId, amount = coupon.amount }, transaction);
                    await connection.ExecuteAsync(query2, new { cid = coupon.cpnId, uid = uid, op = (int)Operation.Void, time = DateTime.Now, cname = coupon.ClientName, cpos = coupon.ClientPos }, transaction);

                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    return -6;
                }
            }
        }


        public async Task<int> ChangeStatus(StatusCoupon coupon, int uid)
        {
            int result;
            var query = "UPDATE Coupons SET cpnStatus = @status, cpnUpdTime = CURRENT_TIMESTAMP WHERE cpnId = @id";
            var query2 = "INSERT INTO CouponLog Values (@cid, @uid, @op, @time, @cname, @cpos)";

            using var connection = _dbConnect.Connect("Command");
            connection.Open();
           
            Enum.TryParse<Operation>(coupon.operation, true, out Operation op);
            Enum.TryParse<Status>(coupon.status, true, out Status sta);
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    result = await connection.ExecuteAsync(query, new { id = coupon.cpnId, status = (int)sta }, transaction);
                
                    result = await connection.ExecuteAsync(query2, new { cid = coupon.cpnId, uid = uid, op = (int)op, time = DateTime.Now, cname = coupon.ClientName, cpos = coupon.ClientPos }, transaction);
                    if (result < 1)
                    {
                        throw new Exception();
                    }
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    return -2;
                }
            }
        }


    }
}
