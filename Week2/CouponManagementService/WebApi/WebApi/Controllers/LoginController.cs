using CouponManagementService.Core.Models;
using CouponManagementService.WebApi.Models;
using CouponManagementService.FrameWork;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;


namespace CouponManagementService.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DatabaseConfig _dbConnect;
        private readonly string _saltkey = "P2.2a";
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public LoginController(DatabaseConfig dbConnect)
        {
            _dbConnect = dbConnect;
           
            
        }

        [HttpPost("Signup")]
        public async Task<BaseResponse<string>> SignUpRequest(User item)
        {
            _logger.Info("Signup Called!");
            var query = "IF NOT EXISTS (SELECT * FROM Users WHERE Username = @username) INSERT INTO Users (Username,Password,Name,Email,Phone) Values (@username,@password,@name,@email,@phone)";
            using var connection = _dbConnect.Connect();
            string pass = hash(item.Password + _saltkey);
            try
            {
                var check = await connection.ExecuteAsync(query, new {username=item.UserName, password= pass, name = item.Name, email = item.Email, phone = item.PhoneNumber});
                if (check == 0 ||check == -1)
                {
                    return new BaseResponse<string>
                    {
                        isSucces = false,
                        statusCode = -4,
                        errorMessage = "There is user exist in this username!",
                        result = "Signup Failed!"
                    };
                }
                return new BaseResponse<string>
                {
                    isSucces = true,
                    statusCode = 1,
                    errorMessage = "",
                    result = "Signup Successfull"
                };
            }
            catch
            {
                return new BaseResponse<string>
                {
                    isSucces = false,
                    statusCode = -3,
                    errorMessage = "Something happend while executing command",
                    result = "Signup Failed!"
                };
            }

           
        }

        [HttpPost]
        public async Task<BaseResponse<User>> LoginRequest(Login item)
        {
            _logger.Info("Login Called!");
            var query = "SELECT * FROM Users WHERE Username = @username";
            using var connection = _dbConnect.Connect();
            try
            {
                var entity = await connection.QueryFirstAsync<User>(query, new { username = item.Username });
                if (entity.Password == hash(item.Password+_saltkey))
                {
                    return new BaseResponse<User>
                    {
                        isSucces = true,
                        statusCode = 1,
                        errorMessage = "",
                        result = entity
                    };
                }
                else
                {
                    return new BaseResponse<User>
                    {
                        isSucces = false,
                        statusCode = -2,
                        errorMessage = "Invalid username or password",
                        result = new User()
                    };
                }

            }
            catch
            {
                return new BaseResponse<User>
                {
                    isSucces = false,
                    statusCode = -2,
                    errorMessage = "Invalid username or password",
                    result = new User()
                };
            }

            
        }

        
    }
}
