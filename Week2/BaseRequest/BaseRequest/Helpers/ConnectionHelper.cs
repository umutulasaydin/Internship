using System.Data;
using System.Data.SqlClient;

namespace BaseRequest.Helpers
{
    public class ConnectionHelper
    {
        private readonly IConfiguration _configuration;

        public ConnectionHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateSqlConnection(string clientName) => new SqlConnection(_configuration.GetConnectionString(clientName));
    }
}
