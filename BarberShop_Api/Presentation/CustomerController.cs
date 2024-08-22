using BarberShop_Api.Application.ViewModel;
using BarberShop_Api.Application.ViewModel.CustomerViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using BarberShop_Api.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


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

        [HttpGet]
        public IActionResult GetCustomersEntity()
        {
            var customers = _customerRepository.Get();

            return Ok(customers);
        }

        [Authorize]
        [HttpGet("own")]
        public IActionResult GetCustomer()
        {
            var customers = _customerRepository.GetByClaim();

            return Ok(customers);
        }

        [Authorize]
        [HttpGet("get-photo")]
        public IActionResult GetPhotoCustomer()
        {
            byte[] photo = [];

            var customer = _customerRepository.GetByClaim();

            try
            {
                if (customer == null)
                {
                    return BadRequest("Customer doesn't exist");
                }
                photo = System.IO.File.ReadAllBytes(customer.Photo);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return NotFound("File not found");
            }
            catch(DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return NotFound("Directory not found");
            }

            return File(photo, "image/jpeg");
        }

        [Authorize]
        [HttpPatch("patch-photo")]
        public IActionResult ChangePhotoCustomer([FromForm] PatchPhoto view)
        {
            var customer = _customerRepository.GetByClaim();

            if(customer == null || view.Photo == null)
            {
                return BadRequest("Customer aren't exist or photo is null");
            }
                        
            try
            {
               string pathPhoto = _customerRepository.UploadArchive(view.Photo, customer.CPF);

                if (!pathPhoto.IsNullOrEmpty())
                {
                    _customerRepository.Patch(customer.Id, pathPhoto, "Photo");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
       
            return Ok();
        }

        [HttpPost]
        public IActionResult AddCustomerEntity([FromForm]CustomerViewPost view)
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
        [HttpDelete]
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

        [HttpPost("verify-doc")]
        public IActionResult VerifyDoc(string document)
        {
            var customers = _customerRepository.Get();
            bool isAvaliable;

            foreach (var customer in customers)
            {
                if (customer.CPF == document)
                {
                    return Ok(false);
                }
            }

            isAvaliable = VerifyDocument.Verify(document);

            return Ok(isAvaliable);
        }


    }
}
