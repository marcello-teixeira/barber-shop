using BarberShop_Api.Application.ViewModel;
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


        [Authorize]
        [HttpGet("get/custumer")]
        public IActionResult GetCustomersEntity()
        {
            var customers = _customerRepository.Get();
            return Ok(customers);
        }



        [HttpPost("post/customer")]
        public IActionResult PostCustomerEntity([FromForm]CustomerViewModel view)
        {
            view.Photo ??= "";

            _customerRepository.Post(new CustomerModel(
                Id: view.Id,
                Name: view.LoginName,
                CPF: view.CPF,
                Photo: view.Photo,
                Email: view.Email,
                Password: view.LoginPassword,
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
