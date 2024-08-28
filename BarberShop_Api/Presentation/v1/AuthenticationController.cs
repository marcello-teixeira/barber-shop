using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.ViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace BarberShop_Api.Presentation.v1
{
    [ApiController]
    [Route("/v{version:ApiVersion}/authentication/")]
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
        public IActionResult AuthenticationCustomer(LoginView view)
        {
            var customers = _customerRepository.Get();
            var companies = _companyRepository.Get();

            // Store in Hash-256 password client sent
            string DecryptPassword = EncryptCode.TransformCode256Hash(view.Password);

            foreach (var customer in customers)
            {
                if ((customer.Name == view.Login || customer.Email == view.Login) && customer.Password == DecryptPassword)
                {
                    // Call the method to get the token and store claims
                    var token = TokenService.GenerateToken(customer);
                    string role = "customer";
                    return Ok(new { token, role });
                }
            }

            foreach (var company in companies)
            {
                if ((company.Name == view.Login || company.Email == view.Login) && company.Password == DecryptPassword)
                {
                    // Call the method to get the token and store claims
                    object token = TokenService.GenerateToken(company);
                    string role = "company";

                    return Ok(new { token, role });
                }
            }

            return BadRequest("User or password invalid");
        }



    }
}
