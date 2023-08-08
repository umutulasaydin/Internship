using CouponManagementServiceV2.Core.Model.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;



namespace CouponManagementServiceV2.Core.Model.Shared
{
    public class Cryption
    {
        public string _saltkey = "P2.2a";

        public string hash(string key)
        {

            SHA256 sHA256 = SHA256.Create();

            byte[] bytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(key));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public string GenerateJwtToken(Users user, string key, string audience, string issuer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.usName),
                new Claim(ClaimTypes.Email, user.usMail),
                new Claim(ClaimTypes.MobilePhone, user.usPhoneNum),
                new Claim(ClaimTypes.NameIdentifier, user.usUsername),
                new Claim(ClaimTypes.PrimarySid, user.usId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IEnumerable<Claim> GetInfoFromToken(string token, string key, string audience, string issuer)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var value = handler.ReadToken(token) as JwtSecurityToken;
                return value.Claims;
            }
            catch
            {
                return null;
            }

        }
    }
}
