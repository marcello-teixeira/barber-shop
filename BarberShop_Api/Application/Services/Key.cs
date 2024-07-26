using System.Text;

namespace BarberShop_Api.Application.Services
{
    public class Key
    {
        private const string Chars = "QWERTYUIOPÇLKJHGFDSAZXCVBNM1234567890abcdefghijklmnopqrstuvwxyz!@#$%¨&*()_+-?:.,";
        public static string Private { get; private set; } = GeneretedKey();

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
