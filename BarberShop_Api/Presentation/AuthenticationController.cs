using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.ViewModel.CustomerViewModel;
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
        private readonly IRepository<CompanyModel> _companyRepository;

        public AuthenticationController(IRepository<CustomerModel> customerRepository, IRepository<CompanyModel> companyRepository)
        {
            _customerRepository = customerRepository ?? throw new Exception("The element customer in Authentication is null");
            _companyRepository = companyRepository ?? throw new Exception("The element company in Authentication is null");
        }


        [Route("CustomerLogin")]
        [HttpPost]
        public IActionResult AuthenticationCustomer([FromForm] CompanyViewLogin view)
        {
            var customers = _customerRepository.Get();

            foreach(var customer in customers)
            {
                if (customer.Name == view.Login && customer.Password == view.Password)
                {
                    object token = TokenService.GenerateTokenCustomer(customer);
                    return Ok(token);
                }
            }

            return BadRequest("User or password invalid");
        }

        [Route("CompanyLogin")]
        [HttpPost]
        public IActionResult AuthenticationCompany([FromForm] CompanyViewLogin view)
        {
            List<CompanyModel> companies = _companyRepository.Get();

            foreach (CompanyModel company in companies)
            {
                if (company.Login == view.Login && company.Password == view.Password)
                {
                    object token = TokenService.GenerateTokenCompany(company);
                    return Ok(token);
                }
            }

            return BadRequest("User or password invalid");
        }
    }
}
