using BarberShop_Api.Application.ViewModel.CompanyViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop_Api.Presentation
{
    public partial class HomeController : ControllerBase
    {
        //
        //  Company Controller 
        //

        [HttpGet("get/company")]
        public IActionResult GetCompanyEntity()
        {
            var companies = _companyRepository.Get();
            return Ok(companies);
        }



        [HttpPost("post/company")]
        public IActionResult AddCompanyEntity([FromForm] CompanyViewPost view)
        {
            string pathString = "Storage/profileDefault.jpg";

            if (view.Photo is not null)
            {
                _companyRepository.UploadArchive(view.Photo, view.CNPJ);
            }


            _companyRepository.Add(new CompanyModel(
                Name: view.Name,
                Location: view.Location,
                Login: view.Login,
                CNPJ: view.CNPJ,
                Photo: pathString,
                Email: view.Email,
                Password: view.Password,
                Phone: view.Phone,
                ActiveAgender: false
                ));
            

            return Ok();
        }

        [HttpDelete("delete/company")]
        public IActionResult DeleteCompanyEntity(int id)
        {
            try
            {
                _companyRepository.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest($"Fail deleted user {e}");
            }

            return Ok($"Customer °{id} has been deleted");
        }

    }
}
