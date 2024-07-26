using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop_Api.Presentation
{
    [Route("/")]
    [ApiController]
    public partial class HomeController : ControllerBase
    {
        private readonly IRepository<CustomerModel> _customerRepository;
        private readonly IRepository<CompanyModel> _companyRepository;

        public HomeController(IRepository<CustomerModel> customerRepository, IRepository<CompanyModel> companyRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }


    }
}
