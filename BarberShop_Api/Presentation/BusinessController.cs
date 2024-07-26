using BarberShop_Api.Application.ViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop_Api.Presentation
{
    public partial class HomeController : ControllerBase
    {
        //
        //  Business Controller 
        //

        [HttpGet("get/business")]
        public IActionResult GetBusinessEntity()
        {
            var business = _businessRepository.Get();
            return Ok(business);
        }



        [HttpPost("post/business")]
        public IActionResult PostBusinessEntity([FromForm] CustomerViewModel view)
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

        [HttpDelete("delete/business")]
        public IActionResult DeleteBusinessEntity(int id)
        {
            try
            {
                _customerRepository.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest($"Fail deleted user {e}");
            }

            return Ok($"Customer °{id} has been deleted");
        }

    }
}
