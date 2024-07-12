using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using BarberShop_Api.Domain.Models;

namespace BarberShop_Api.Application.Services
{
    public class TokenService
    {
        public static object GenerateTokenCustomer(CustomerModel entity)
        {
            byte[] key = Encoding.Default.GetBytes(Key.Private);

            var OptionsToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim("CustomerId", entity.Id.ToString()),
                    new Claim("CustomerName", entity.LoginName.ToString()),
                    new Claim("Acess", entity.CPF.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(50),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var HandlerToken = new JwtSecurityTokenHandler();
            var token = HandlerToken.CreateToken(OptionsToken);
            string Secrettoken = HandlerToken.WriteToken(token);

            return new { Secrettoken };
        }

        public static object GenerateTokenBusiness(BusinessModel entity)
        {
            byte[] key = Encoding.Default.GetBytes(Key.Private);

            var OptionsToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("Customer_Id", entity.Id.ToString()),
                    new Claim("Customer_Name", entity.LoginName.ToString()),
                    new Claim("Acess", entity.CNPJ.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(50),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var HandlerToken = new JwtSecurityTokenHandler();
            var token = HandlerToken.CreateToken(OptionsToken);
            string Secrettoken = HandlerToken.WriteToken(token);

            return new { Secrettoken };
        }



    }
}
