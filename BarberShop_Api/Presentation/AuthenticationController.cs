using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.ViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop_Api.Presentation
{
    [ApiController]
    [Route("/authentication/")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IRepository<CustomerModel> _customerRepository;
        private readonly IRepository<CompanyModel> _companyRepository;

        public AuthenticationController(IRepository<CustomerModel> customerRepository, IRepository<CompanyModel> companyRepository)
        {
            _customerRepository = customerRepository ?? throw new Exception("The element customer in Authentication is null");
            _companyRepository = companyRepository ?? throw new Exception("The element company in Authentication is null");
        }


        [Route("login")]
        [HttpPost]
        public IActionResult AuthenticationCustomer(ViewLogin view)
        {
            var customers = _customerRepository.Get();
            var companies = _companyRepository.Get();

            foreach(var customer in customers)
            {
                if (customer.Name == view.Login && customer.Password == view.Password)
                {
                    var token = TokenService.GenerateTokenCustomer(customer);

                    return Ok(token);
                }
            }

            foreach(var company in companies)
            {
                if (company.Name == view.Login  && company.Password == view.Password)
                {
                    object token = TokenService.GenerateTokenCustomer(company);
                    return Ok(token);
                }
            }

            return BadRequest("User or password invalid");
        }

    }
}
