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
            var op = -1;
            if (coupon.cpnStatus == 1)
            {
                op = 5;
            }
            else
            {
                op = 4;
            }
            using var connection = _dbConnect.Connect("Command");
            connection.Open();
            
           
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    
                    result = await connection.QueryFirstAsync<int>(query, new { serieId = coupon.cpnSerieId, CreatorId = coupon.cpnCreatorId, code = coupon.cpnCode, status = coupon.cpnStatus, start = coupon.cpnStartDate, valid = coupon.cpnValidDate, redemptLimit = coupon.cpnRedemptionLimit, currentLimit = coupon.cpnRedemptionLimit, ins = coupon.cpnInsTime, upd = coupon.cpnUpdTime }, transaction);
 
                    await connection.ExecuteAsync(query2, new { uid = coupon.cpnCreatorId, op = op, cname = coupon.ClientName, clpos = coupon.ClientPos }, transaction);
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
            var query1 = "DECLARE @id INT;EXEC dbo.GetSeriesId @serieId, @serieName, @serieDesc, @count, @id OUTPUT;SELECT @id";
            var query2 = "INSERT INTO Coupons (cpnSerieId, cpnCreatorId, cpnCode, cpnStatus, cpnStartDate, cpnValidDate, cpnRedemptionLimit, cpnCurrentRedemptValue, cpnInsTime, cpnUpdTime) Values (@serieId, @creatorId, @code, @status, @start, @valid, @redemptLimit, @currentLimit, @ins, @upd);SELECT @@IDENTITY";
            var query3 = "INSERT INTO CouponLog (cplCouponId, cplUserId, cplOperation, cplClientName, cplClientPos) VALUES (@@IDENTITY, @uid, @op, @cname, @clpos)";
            using var connection = _dbConnect.Connect("Command");
            connection.Open();
  
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var cpnId = 0;
                    var cpsId = await connection.QueryFirstAsync<int>(query1, new { serieId = series.cpsSeriesId, serieName = series.cpsSerieName, serieDesc = series.cpsSerieDesc, count = series.cpsSerieCount }, transaction);
                    

                    for (int i = 0; i< series.cpsSerieCount; i++)
                    {
                        string code = Guid.NewGuid().ToString();
                        
                        cpnId = await connection.QueryFirstAsync<int>(query2, new { serieId = cpsId, CreatorId = series.cpsUserId, code = code, status = series.cpsStatus, start = series.cpsStartDate, valid = series.cpsValidDate, redemptLimit = series.cpsRedemptionLimit, currentLimit = series.cpsRedemptionLimit, ins = series.cpsInsTime, upd = series.cpsUpdTime }, transaction);
                        
                      
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
                            cpnCurrentRedemptValue = series.cpsRedemptionLimit,
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
            var query = "DECLARE @result INT;EXEC dbo.Redemption @couponid, @uid, @cname, @cpos, @amount, @result OUTPUT;SELECT @result";

            using var connection = _dbConnect.Connect("Command");
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    result = await connection.QueryFirstAsync<int>(query, new { couponid = coupon.cpnId, uid = uid, cname = coupon.ClientName, cpos = coupon.ClientPos, amount = coupon.amount}, transaction);
          
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
            var query = "DECLARE @result INT;EXEC dbo.Void @couponid, @uid, @cname, @cpos, @amount, @result OUTPUT;SELECT @result";
            

            using var connection = _dbConnect.Connect("Command");
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    result = await connection.QueryFirstAsync<int>(query, new { couponid = coupon.cpnId, uid = uid, cname = coupon.ClientName, cpos = coupon.ClientPos, amount = coupon.amount }, transaction);

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
          
            var query = "DECLARE @result INT;EXEC dbo.ChangeStatus @couponId, @newStatus, @uid, @cname, @cpos, @result OUTPUT;SELECT @result";
            using var connection = _dbConnect.Connect("Command");
            connection.Open();
           
            
            
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    result = await connection.QueryFirstAsync<int>(query, new { couponId = coupon.cpnId, newStatus = coupon.status, uid = uid, cname = coupon.ClientName, cpos = coupon.ClientPos}, transaction);
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    return -5;
                }
            }
        }


    }
}
