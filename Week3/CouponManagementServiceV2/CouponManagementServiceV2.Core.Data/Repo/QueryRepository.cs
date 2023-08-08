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
    }
}
