using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.ViewModel.CustomerViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;


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
        [HttpGet]
        public IActionResult GetCustomersEntity()
        {
            var customers = _customerRepository.Get();

            return Ok(customers);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customers = _customerRepository.Get(id);

            return Ok(customers);
        }

        [Authorize]
        [HttpGet("photo/{id}")]
        public IActionResult GetPhotoCustomer(int id)
        {
            byte[] photo = Encoding.Default.GetBytes("");

            var customer = _customerRepository.Get(id);
            

            try
            {
                if (customer == null || Directory.Exists(customer.Photo))
                {
                    return BadRequest();
                }
                photo = System.IO.File.ReadAllBytes(customer.Photo);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            return File(photo, "image/jpeg");
        }

        [Authorize]
        [HttpPatch("photo")]
        public IActionResult ChangePhotoCustomer([FromForm] CustomerPatchPhoto view)
        {
            var claims = TokenService.GetClaims();

            if( claims == null)
            {
                return BadRequest("Claims is null");
            }

            string customerCPF = claims.First(claim => claim.Type == "CPF").Value;
            int customerID = Convert.ToInt32(claims.First(claim => claim.Type == "Id").Value);
            
            string pathPhoto = "";

            try
            {
                if (view.Photo == null)
                {
                    return BadRequest("Customer or photo is null");
                }

                pathPhoto = _customerRepository.UploadArchive(view.Photo, customerCPF);

                if (!pathPhoto.IsNullOrEmpty())
                {
                    _customerRepository.Patch(customerID, pathPhoto, "Photo");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
       
            return Ok();
        }

        [HttpPost]
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


    }
}
