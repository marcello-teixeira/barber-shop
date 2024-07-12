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
        private readonly IRepository<BusinessModel> _businessRepository;

        public HomeController(IRepository<CustomerModel> customerRepository, IRepository<BusinessModel> businessRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _businessRepository = businessRepository ?? throw new ArgumentNullException(nameof(businessRepository));
        }


    }
}
