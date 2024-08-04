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

    public partial class HomeController : ControllerBase
    {
        //
        // Customer Controller
        //

        [Authorize]
        [HttpGet("get/customer")]
        public IActionResult GetCustomersEntity()
        {
            //var customers = _customerRepository.Get();

            var claim = TokenService.GetClaims();

            return Ok(claim);
        }



        [HttpPost("post/customer")]
        public  IActionResult  AddCustomerEntity([FromForm]CustomerViewPost view)
        {
            string pathPhoto = "Storage/profileDefault";

            if (view.Photo is not null)
            {
                pathPhoto = _companyRepository.UploadArchive(view.Photo, view.CPF);
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
        [HttpDelete("delete/customer")]
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
