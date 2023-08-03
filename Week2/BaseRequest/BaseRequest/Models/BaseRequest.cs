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
        

       

        public async Task<BaseResponse<Student>> getAll(ConnectionHelper _connectionHelper)
        {
            
            var query = "SELECT * FROM Student";
            using var connection = _connectionHelper.CreateSqlConnection(dataset);
            
            try
            {
                var list = await connection.QueryAsync<Student>(query);
                if (!list.Any())
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
                    result = list.ToList()
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

        public async Task<BaseResponse<Student>> getOne(ConnectionHelper connectionHelper, int id)
        {
            var query = "SELECT * FROM Student WHERE StudentId = @Id";
            
            using var connection = connectionHelper.CreateSqlConnection(dataset);
               
            try
            {
                var entity = await connection.QueryFirstAsync<Student>(query, new { Id = id });

                return new BaseResponse<Student>
                {
                    isSucces = true,
                    statusCode = 1,
                    errorMessage = "",
                    result = new List<Student> { entity }
                };
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Sequence contains no elements", StringComparison.InvariantCultureIgnoreCase))
                {
                    return new BaseResponse<Student>
                    {
                        isSucces = false,
                        statusCode = -1,
                        errorMessage = "There is no entity in this ID",
                        result = new List<Student>()
                    };
                }
                else
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

        public async Task<BaseResponse<Student>> Insert(ConnectionHelper connectionHelper, Student item)
        {
            var query = "INSERT INTO Student (StudentId,Name,Roll,Message) VALUES (@id,@name,@roll,@message)";
            using var connection = connectionHelper.CreateSqlConnection(dataset);
            try
            {
                var check = await connection.ExecuteAsync(query, new { id = item.StudentId, name = item.Name, roll = item.Roll, message = item.message });
                if (check == 0)
                {
                    return new BaseResponse<Student>
                    {
                        isSucces = false,
                        statusCode = -4,
                        errorMessage = "Insertion is failed!",
                        result = new List<Student>()
                    };
                }
                return new BaseResponse<Student>
                {
                    isSucces = true,
                    statusCode = 1,
                    errorMessage = "",
                    result = new List<Student>()
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

        public async Task<BaseResponse<Student>> Delete(ConnectionHelper connectionHelper, int id)
        {
            var query = "DELETE FROM Student WHERE StudentId = @Id";
            using var connection = connectionHelper.CreateSqlConnection(dataset);
            var check = await connection.ExecuteAsync(query, new { Id = id });


            if (check == 0)
            {
                return new BaseResponse<Student>
                {
                    isSucces = false,
                    statusCode = -1,
                    errorMessage = "There is no entity in this ID",
                    result = new List<Student>()
                };
            }
            return new BaseResponse<Student>
            {
                isSucces = true,
                statusCode = 1,
                errorMessage = "",
                result = new List<Student>()
            };

        }
    }

    public class AddItem
    {
        public XRequest request { get; set; }
        public Student student { get; set; }
       
    }


}
