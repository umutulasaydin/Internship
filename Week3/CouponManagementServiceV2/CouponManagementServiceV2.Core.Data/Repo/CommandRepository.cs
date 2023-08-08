using CouponManagementServiceV2.Core.Data.Interfaces;
using CouponManagementServiceV2.Core.Model.Shared;
using CouponManagementServiceV2.Core.Model.Data;

using Dapper;
using System.Net;
using System;

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
                var query = "IF NOT EXISTS (SELECT * FROM Users WHERE usUsername = @username) INSERT INTO Users (usId, usUsername,usPassword,usName,usMail,usPhoneNum) Values (@id, @username,@password,@name,@email,@phone)";
                using var connection = _dbConnect.Connect("Command");
                string pass = _crypte.hash(item.usPassword + _crypte._saltkey);
                result = await connection.ExecuteAsync(query, new { id = item.usId, username = item.usUsername, password = pass, name = item.usName, email = item.usMail, phone = item.usPhoneNum });
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
            try
            {
                var query = "INSERT INTO Coupons (cpnId, cpnSerieId, cpnCreatorId, cpnCode, cpnStatus, cpnStartDate, cpnValidDate, cpnRedemptionLimit, cpnCurrentRedemptValue, cpnInsTime, cpnUpdTime) Values (@id, @serieId, @creatorId, @code, @status, @start, @valid, @redemptLimit, @currentLimit, @ins, @upd)";
                using var connection = _dbConnect.Connect("Command");
                result = await connection.ExecuteAsync(query, new { id = coupon.cpnId, serieId = coupon.cpnSerieId, CreatorId = coupon.cpnCreatorId, code = coupon.cpnCode, status = coupon.cpnStatus, start = coupon.cpnStartDate, valid = coupon.cpnValidDate, redemptLimit = coupon.cpnRedemptionLimit, currentLimit = coupon.cpnCurrentRedemptValue, ins = coupon.cpnInsTime, upd = coupon.cpnUpdTime });
            }
            catch { result = -2; }
            return result;
        }
    }
}
