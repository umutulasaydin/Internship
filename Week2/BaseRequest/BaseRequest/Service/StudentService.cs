using BaseRequest.Helpers;
using BaseRequest.BaseRequest;
using BaseRequest.Models;
using Dapper;
using System.Net.Http.Headers;

namespace BaseRequest.Service
{
    public class StudentService : BaseRequest<BaseResponse<Student>, Student>
    {

        private readonly ConnectionHelper _connectionHelper;
        public StudentService(ConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;

        }
        public BaseResponse<Student> Delete(int id, string _client)
        {
            var query = "DELETE FROM Student WHERE StudentId = @Id";
            using var connection = _connectionHelper.CreateSqlConnection(_client);
            var check = connection.Execute(query, new { Id = id });


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

        public BaseResponse<Student> GetAll(string _client)
        {
            var query = "SELECT * FROM Student";
            using var connection = _connectionHelper.CreateSqlConnection(_client);
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

        public BaseResponse<Student> GetById(int id, string _client)
        {
            var query = "SELECT * FROM Student WHERE StudentId = @Id";
            using var connection = _connectionHelper.CreateSqlConnection(_client);
            try
            {
                var entity = connection.QueryFirst<Student>(query, new { Id = id });
                
                return new BaseResponse<Student>
                {
                    isSucces = true,
                    statusCode = 1,
                    errorMessage = "",
                    result = new List<Student> { entity }
                };
            }
            catch
            {
                return new BaseResponse<Student>
                {
                    isSucces = false,
                    statusCode = -1,
                    errorMessage = "There is no entity in this ID",
                    result = new List<Student>()
                };
            }

        }

        public BaseResponse<Student> Insert(Student item, string _client)
        {
            var query = "INSERT INTO Student (StudentId,Name,Roll,Message) VALUES (@id,@name,@roll,@message)";
            using var connection = _connectionHelper.CreateSqlConnection(_client);
            try
            {
                var check = connection.Execute(query, new { id = item.StudentId, name = item.Name, roll = item.Roll, message = item.message });
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
    }
}
