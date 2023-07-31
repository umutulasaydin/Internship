using BaseRequest.Helpers;
using BaseRequest.BaseRequest;
using BaseRequest.Models;
using Dapper;
using System.Net.Http.Headers;

namespace BaseRequest.Service
{
    public class TeacherService : BaseRequest<BaseResponse<Teacher>, Teacher>
    {

        private readonly ConnectionHelper _connectionHelper;
        public TeacherService(ConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;

        }
        public BaseResponse<Teacher> Delete(int id, string _client)
        {
            var query = "DELETE FROM Teacher WHERE TeacherId = @Id";
            using var connection = _connectionHelper.CreateSqlConnection(_client);
            var check = connection.Execute(query, new { Id = id });


            if (check == 0)
            {
                return new BaseResponse<Teacher>
                {
                    isSucces = false,
                    statusCode = -1,
                    errorMessage = "There is no entity in this ID",
                    result = new List<Teacher>()
                };
            }
            return new BaseResponse<Teacher>
            {
                isSucces = true,
                statusCode = 1,
                errorMessage = "",
                result = new List<Teacher>()
            };

        }

        public BaseResponse<Teacher> GetAll(string _client)
        {
            var query = "SELECT * FROM Teacher";
            using var connection = _connectionHelper.CreateSqlConnection(_client);
            try
            {
                var list = connection.Query<Teacher>(query).ToList();
                if (list.Count == 0)
                {
                    return new BaseResponse<Teacher>
                    {
                        isSucces = false,
                        statusCode = -2,
                        errorMessage = "Query is empty",
                        result = new List<Teacher>()
                    };
                }
                return new BaseResponse<Teacher>
                {
                    isSucces = true,
                    statusCode = 1,
                    errorMessage = "",
                    result = list
                };
            }
            catch
            {
                return new BaseResponse<Teacher>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Something happend while executing command",
                    result = new List<Teacher>()
                };
            }

        }

        public BaseResponse<Teacher> GetById(int id, string _client)
        {
            var query = "SELECT * FROM Teacher WHERE TeacherId = @Id";
            using var connection = _connectionHelper.CreateSqlConnection(_client);
            try
            {
                var entity = connection.QueryFirst<Teacher>(query, new { Id = id });
                
                return new BaseResponse<Teacher>
                {
                    isSucces = true,
                    statusCode = 1,
                    errorMessage = "",
                    result = new List<Teacher> { entity }
                };
            }
            catch
            {
                return new BaseResponse<Teacher>
                {
                    isSucces = false,
                    statusCode = -1,
                    errorMessage = "There is no entity in this ID",
                    result = new List<Teacher>()
                };
            }

        }

        public BaseResponse<Teacher> Insert(Teacher item, string _client)
        {
            var query = "INSERT INTO Teacher (TeacherId,Name,Subject,Message) VALUES (@id,@name,@subject,@message)";
            using var connection = _connectionHelper.CreateSqlConnection(_client);
            try
            {
                var check = connection.Execute(query, new { id = item.TeacherId, name = item.Name, subject = item.Subject, message = item.message });
                if (check == 0)
                {
                    return new BaseResponse<Teacher>
                    {
                        isSucces = false,
                        statusCode = -4,
                        errorMessage = "Insertion is failed!",
                        result = new List<Teacher>()
                    };
                }
                return new BaseResponse<Teacher>
                {
                    isSucces = true,
                    statusCode = 1,
                    errorMessage = "",
                    result = new List<Teacher>()
                };
            }
            catch
            {
                return new BaseResponse<Teacher>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Something happend while executing command",
                    result = new List<Teacher>()
                };
            }
            
        }
    }
}
