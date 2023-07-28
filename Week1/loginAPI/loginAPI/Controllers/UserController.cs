using loginAPI.Data;
using loginAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace loginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _conf;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMemoryCache _cache;
        private readonly string _saltkey;

        public UserController(IConfiguration conf, ILogger<UserController> logger, IMemoryCache cache, ApplicationDbContext db)
        {
            _saltkey = "?A5kl3.";
            _conf = conf;
            _logger = logger;
            _db = db;
            _cache = cache;
        }

        [HttpPost("SignUpRequest")]
        public IActionResult SignupRequest(UserSignUp sign)
        {
            _logger.LogInformation("Signup called!");
            var user = _db.Users.FirstOrDefault(o => o.username == sign.username);

            if (user == null)
            {
                user = new User
                {
                    username = sign.username,
                    password = hash(sign.password+_saltkey),
                    email = sign.email,
                    name = sign.name,
                    surname = sign.surname,
                    phone = sign.phone,
                    refreshToken = "",
                    expiryTime = DateTime.Now,
                    active = false
                };
                _db.Users.Add(user);
                _db.SaveChanges();
                _logger.LogInformation("Signup successfull!");
                return Ok("Sign Up Successfull!");
            }
            _logger.LogInformation("Signup failed!");
            return BadRequest("Sign Up Failed");
        }

     
        [HttpPost("LoginRequest")]
        public LoginResponse LoginRequest(UserLogin login)
        {
            _logger.LogInformation("Login called!");
            var user = _db.Users.FirstOrDefault(o => o.username == login.username && o.password == hash(login.password+_saltkey));

            if (user == null)
            {
                _logger.LogInformation("Login failed!");
                return null;
            }

            if (user.active == false)
            {
                var refreshToken = GenerateRefreshToken();
                user.refreshToken = hash(refreshToken + _saltkey);
                user.expiryTime = DateTime.Now.AddHours(1);
                user.active = true;
                _db.Update(user);
                _db.SaveChanges();


                if (!_cache.TryGetValue(user.username, out string token))
                {
                    _logger.LogInformation("There is no token in cache!");
                    token = GenerateJwtToken(user);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    _cache.Set(user.username, token, cacheEntryOptions);
                    _logger.LogInformation("Token created. " + DateTime.Now.ToString() + "!");
                }
                else
                {
                    _logger.LogInformation("There is a token in cache. Time is extended " + DateTime.Now.ToString() + "!");
                }

                Tokens tokens = new Tokens();
                tokens.accessToken = token;
                tokens.refreshToken = refreshToken;
                LoginResponse loginResponse = new LoginResponse();
                loginResponse.tokens = tokens;
                loginResponse.user = user;
                return loginResponse;
            }

            return null;
        }

        [HttpPost("LogoutRequest")]
        public IActionResult LogoutRequest([FromBody]string token)
        {
            _logger.LogInformation("Logout called!");
            string username = GetInfoFromToken(token);

            if (_cache.TryGetValue(username, out string value))
            {
                _logger.LogInformation("There is a token in cache!");
                if (value == token)
                {
                    var user = _db.Users.FirstOrDefault(o => o.username == username);
                    user.active = false;
                    user.refreshToken = "";
                    user.expiryTime = DateTime.Now;
                    _db.Users.Update(user);
                    _db.SaveChanges();
                    _logger.LogInformation("Matched!");
                    return Ok("Logout successful");
                }
                else
                {
                    _logger.LogInformation("Not matched!");
                    return Unauthorized("Token is expired");
                }
            }
            _logger.LogInformation("There is no token in cache!");
            return Unauthorized("Token is expired");
        }
    

        [HttpGet("PingRequest")]
        public IActionResult PingRequest()
        {
            _logger.LogInformation("Ping!");
            return Ok("Pong");
        }

        [HttpPost("RefreshRequest")]
        public IActionResult RefreshRequest(Tokens tokens)
        {
            _logger.LogInformation("Refresh called!");
            string accessToken = tokens.accessToken;
            string refreshToken = tokens.refreshToken;

            string username = GetInfoFromToken(accessToken);

            var user = _db.Users.FirstOrDefault(o=> o.username == username);

            if (user == null || user.refreshToken != hash(refreshToken + _saltkey) || user.expiryTime <= DateTime.Now || user.active == false) 
            {
                _logger.LogInformation("Invalid tokens!");
                return BadRequest("Invalid tokens");
            }

            var newAccessToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.refreshToken = hash(newRefreshToken + _saltkey);
            user.expiryTime = DateTime.Now.AddHours(1);

            _db.Users.Update(user);
            _db.SaveChanges();

            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(username, newAccessToken, cacheEntryOptions);
            _logger.LogInformation("Renewed token cached "+ DateTime.Now.ToString() + "!");
            IDictionary<string, string> newTokens = new Dictionary<string, string>
            {
                { "accessToken", newAccessToken },
                { "refreshToken", newRefreshToken }
            };
            return Ok(newTokens);
        }

       
        [HttpPost("CheckTokenRequest")]
        public IActionResult CheckToken([FromBody] string token)
        {
            _logger.LogInformation("Check called!");
            string username = GetInfoFromToken(token);

            if (_cache.TryGetValue(username, out string value))
            {
                _logger.LogInformation("There is a token in cache!");
                if (value == token)
                {
                    _logger.LogInformation("Matched!");
                    return Ok("Token is not expired");
                }
                else
                {
                    _logger.LogInformation("Not matched!");
                    return Ok("Token is expired");
                }
            }
            _logger.LogInformation("There is no token in cache!");
            return Ok("Token is expired");
        }
        private string hash(string key)
        {
            _logger.LogInformation("Hash called!");
            SHA256 sHA256 = SHA256.Create();

            byte[] bytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(key));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        private string GenerateJwtToken(User user)
        {
            _logger.LogInformation("Jwt token generator called!");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.username)
            };

            var token = new JwtSecurityToken(_conf["Jwt:Issuer"],
                _conf["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            _logger.LogInformation("Refresh token generator called!");
            var rnd = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(rnd);
            return Convert.ToBase64String(rnd);
        }

        private string GetInfoFromToken(string token)
        {
            _logger.LogInformation("Get username from token called!");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidIssuer = _conf["Jwt:Issuer"],
                ValidAudience = _conf["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["Jwt:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal.Identity.Name;
        }


    }
}




