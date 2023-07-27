using System.ComponentModel.DataAnnotations;

namespace loginAPI.Models
{
    public class User
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }

        public string refreshToken { get; set; }

        public DateTime expiryTime { get; set; }
    }

    public class UserLogin
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
    }

    public class Tokens
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
