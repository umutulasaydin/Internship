using System.ComponentModel.DataAnnotations;

namespace loginAPI.Models
{
    //Active passive bit version
    public class User
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string refreshToken { get; set; }
        public DateTime expiryTime { get; set; }
        public Boolean active { get; set; }
    }

    public class UserSignUp
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
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

    public class LoginResponse
    {
        public Tokens tokens { get; set; }
        public User user { get; set; }
    }
}
