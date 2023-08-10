using System.Data;
using System.Data.SqlClient;

namespace CouponManagementServiceV2.Core.Model.Shared
{
    public class DatabaseConfig
    {
        private readonly IConfiguration _configuration;

        public DatabaseConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connect(string connectionString) => new SqlConnection(_configuration.GetConnectionString(connectionString));
       
    }
}
