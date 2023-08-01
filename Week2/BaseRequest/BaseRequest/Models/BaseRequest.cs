using BaseRequest.Helpers;
using Dapper;

namespace BaseRequest.Models
{
    public class BaseRequest
    {
        public string dataset { get; set; }
        public string clientCode { get; set; }
        public string posName { get; set; }
    }
    public class XRequest : BaseRequest
    {
        

       

        public BaseResponse<Student> get(ConnectionHelper _connectionHelper)
        {
            
            var query = "SELECT * FROM Student";
            using var connection = _connectionHelper.CreateSqlConnection(dataset);
            try
            {
                var list = connection.Query<Student>(query).ToList();
                if (list.Count == 0)
                {
                    return new BaseResponse<Student>
                    {
                        isSucces = false,
                        statusCode = -2,
                        errorMessage = "Query is empty",
                        result = new List<Student>()
                    };
                }
                return new BaseResponse<Student>
                {
                    isSucces = true,
                    statusCode = 1,
                    errorMessage = "",
                    result = list
                };
            }
            catch
            {
                return new BaseResponse<Student>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Something happend while executing command",
                    result = new List<Student>()
                };
            }
        }
    }


}
