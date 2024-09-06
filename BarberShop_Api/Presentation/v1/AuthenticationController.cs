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

            // Get password client sent by view to equals password storaged
            string DecryptPassword = EncryptCode.TransformCode256Hash(view.Password);
            string role = view.Role;

            if(view.Role == "customer")
            {
                foreach (var customer in customers)
                {
                    if (customer.Email == view.Login && customer.Password == DecryptPassword)
                    {
                        // Call the method to get the token and store claims
                        var token = TokenService.GenerateToken(customer);

                        return Ok(new { token, role });
                    }
                }
            }

            if (view.Role == "company")
            {
                foreach (var company in companies)
                {
                    if (company.Email == view.Login && company.Password == DecryptPassword)
                    {
                        // Call the method to get the token and store claims
                        object token = TokenService.GenerateToken(company);

                        return Ok(new { token, role });
                    }
                }
            }

            return BadRequest("User or password invalid");
        }



    }
}
