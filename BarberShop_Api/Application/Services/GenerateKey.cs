using System.Text;

namespace BarberShop_Api.Application.Services
{
    public class GenerateKey
    {
        private const string Chars = "QWERTYUIOPÇLKJHGFDSAZXCVBNM1234567890abcdefghijklmnopqrstuvwxyz!@#$%¨&*()_+-?:.,";
        /// <summary>
        /// Store the private key
        /// </summary>
        public static string Private { get; private set; } = GeneretedKey();

        /// <summary>
        ///  Make a random key asynchronous
        /// </summary>
        private static string GeneretedKey()
        {
            StringBuilder BuilderKey = new();

            Random random = new();

            for (int i = 0; i < Chars.Length; i++)
            {
                int index = random.Next(Chars.Length);

                BuilderKey.Append(Chars[index]);

            }

            return BuilderKey.ToString();
        }
    }
}
