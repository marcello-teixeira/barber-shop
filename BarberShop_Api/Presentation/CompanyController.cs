using BarberShop_Api.Application.ViewModel.CompanyViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop_Api.Presentation
{
    [Route("/company/")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IRepository<CompanyModel> _companyRepository;

        public CompanyController(IRepository<CompanyModel> companyRepository)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }

        [HttpGet("get")]
        public IActionResult GetCompanyEntity()
        {
            var companies = _companyRepository.Get();
            return Ok(companies);
        }



        [HttpPost("post")]
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

        [HttpDelete("delete")]
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
