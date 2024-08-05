using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.ViewModel.CustomerViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Permissions;

namespace BarberShop_Api.Presentation
{
    [Route("/customer/")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly IRepository<CustomerModel> _customerRepository;

        public CustomerController(IRepository<CustomerModel> customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        [Authorize]
        [HttpGet("get")]
        public IActionResult GetCustomersEntity()
        {
            var customers = _customerRepository.Get();

            return Ok(customers);
        }



        [HttpPost("post")]
        public  IActionResult  AddCustomerEntity([FromForm]CustomerViewPost view)
        {
            string pathPhoto = "Storage/profileDefault";

            if (view.Photo is not null)
            {
                pathPhoto = _customerRepository.UploadArchive(view.Photo, view.CPF);
            }

            _customerRepository.Add(new CustomerModel(
                Name: view.Name,
                CPF: view.CPF,
                Photo: pathPhoto,
                Email: view.Email,
                Password: view.Password,
                Phone: view.Phone
            ));

            return Ok();
        }

        [Authorize]
        [HttpDelete("delete")]
        public IActionResult DeleteCustomerEntity(int id)
        {
            try
            {
                _customerRepository.Delete(id);
            }
            catch(Exception e)
            {
                return BadRequest($"Fail deleted user {e}");
            }
                       
            return Ok($"Customer °{id} has been deleted");
        }


    }
}
