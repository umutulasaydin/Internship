using System.Security.Cryptography;
using System.Text;

namespace CouponManagementService.FrameWork.Hashing
{
    public class Hashing
    {

        private string hash(string key)
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
    }
}
