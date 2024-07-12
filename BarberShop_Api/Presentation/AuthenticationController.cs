using BarberShop_Api.Application.Services;
using BarberShop_Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop_Api.Presentation
{
    [ApiController]
    [Route("/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        [Route("CustomerLogin")]
        [HttpPost]
        public IActionResult AuthenticationCustomer()
        {
            object token = TokenService.GenerateTokenCustomer(new CustomerModel(2, "m", "m", "m", "m", "m", "m"));

            return Ok(token);
        }
    }
}
