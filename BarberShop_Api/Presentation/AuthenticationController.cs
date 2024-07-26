using BarberShop_Api.Application.Services;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop_Api.Presentation
{
    [ApiController]
    [Route("/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IRepository<CustomerModel> _customerRepository;

        public AuthenticationController(IRepository<CustomerModel> customerModel)
        {
            _customerRepository = customerModel ?? throw new Exception("The element customer in Authentication is null");
        }


        [Route("CustomerLogin")]
        [HttpPost]
        public IActionResult AuthenticationCustomer()
        {
            var customers = _customerRepository.Get();
            
            object token = TokenService.GenerateTokenCustomer(new CustomerModel(2, "m", "m", "m", "m", "m", "m"));

            return Ok(customers);
        }
    }
}
