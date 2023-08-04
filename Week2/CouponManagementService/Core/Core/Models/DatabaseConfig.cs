using System.Data;
using System.Data.SqlClient;

namespace CouponManagementService.Core.Models
{
    public class DatabaseConfig
    {
        private readonly IConfiguration _configuration;

        public DatabaseConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connect() => new SqlConnection(_configuration.GetConnectionString("Users"));
    }
}
