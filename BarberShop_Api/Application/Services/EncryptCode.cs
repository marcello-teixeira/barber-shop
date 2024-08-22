using System.Security.Cryptography;
using System.Text;

namespace BarberShop_Api.Application.Services
{
    public class EncryptCode
    {
        public static string TransformCode256Hash(string code)
        {
            SHA256 sha256 = SHA256.Create();

            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(code));
            StringBuilder stringBuilder = new();
            
            foreach (var b in bytes)
            {
                stringBuilder.Append(b.ToString("x2"));      
            }
            return stringBuilder.ToString();
        }
    }
}
