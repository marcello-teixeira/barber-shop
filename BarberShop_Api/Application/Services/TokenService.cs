using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using BarberShop_Api.Domain.Models;
using System.Collections.Generic;

namespace BarberShop_Api.Application.Services
{
    public class TokenService
    {
        private static string? tokenJwt;

        public static object GenerateTokenCustomer<T>(T entity)
        {
            byte[] key = Encoding.Default.GetBytes(GenerateKey.Private);

            List<Claim> claims = new();

            foreach(var prop in typeof(T).GetProperties())
            {
                var value = prop.GetValue(entity)?.ToString();
                if (value != null)
                {
                    claims.Add(new Claim(prop.Name, value));
                }
            }

            var OptionsToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(50),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var HandlerToken = new JwtSecurityTokenHandler();
            var token = HandlerToken.CreateToken(OptionsToken);

            tokenJwt = HandlerToken.WriteToken(token);

            return new { tokenJwt };
        }

        public static IEnumerable<Claim> GetClaims()
        {
            if(tokenJwt == null)
            {
                return [];
            }

            var HandlerToken = new JwtSecurityTokenHandler();
            var GetToken = HandlerToken.ReadJwtToken(tokenJwt);

            return GetToken.Claims;
        }
    }
}
