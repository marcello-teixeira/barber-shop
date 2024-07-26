using BarberShop_Api.Application.ViewModel.CustomerViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace BarberShop_Api.Presentation
{

    public partial class HomeController : ControllerBase
    {
        //
        // Customer Controller
        //


        //[Authorize]
        [HttpGet("get/custumer")]
        public IActionResult GetCustomersEntity()
        {
            var customers = _customerRepository.Get();
            return Ok(customers);
        }



        [HttpPost("post/customer")]
        public IActionResult AddCustomerEntity([FromForm]CustomerViewPost view)
        {

            _customerRepository.Add(new CustomerModel(
                Name: view.Name,
                CPF: view.CPF,
                Photo: view.Photo ??= "picture",
                Email: view.Email,
                Password: view.Password,
                Phone: view.Phone
            ));

            return Ok();
        }

        [HttpDelete("delete/custumer")]
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
