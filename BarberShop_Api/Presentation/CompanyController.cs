using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.ViewModel.CompanyView;
using BarberShop_Api.Application.ViewModel.CompanyViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet]
        public IActionResult GetCompanyEntity()
        {
            var companies = _companyRepository.Get();
            return Ok(companies);
        }

        [HttpGet("own")]
        public IActionResult GetCompany()
        {
            var company = _companyRepository.GetByClaim();

            return Ok(company);
        }

        [HttpGet("get-photo")]
        public IActionResult GetPhotoCompany()
        {
            var company = _companyRepository.GetByClaim();
            byte[] bytePhoto = [];

            try
            {
               if(company == null)
               {
                  return BadRequest("Company or photo doesn't exist");
               }

               bytePhoto = System.IO.File.ReadAllBytes(company.Photo);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return NotFound("File not found");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return NotFound("Directory not found");
            }

            return File(bytePhoto, "image/jpg");
        }

        [HttpPatch("patch-photo")]
        public IActionResult PatchPhoto([FromForm] CompanyPatchPhoto view)
        {
            var company = _companyRepository.GetByClaim();

            if (company == null || view.Photo == null)
            {
                return BadRequest("Client or photo sent is null");
            }

            try
            {   
                string pathPhoto = _companyRepository.UploadArchive(view.Photo, company.CNPJ);

                if (!pathPhoto.IsNullOrEmpty())
                {
                    _companyRepository.Patch(company.Id, pathPhoto, "Photo");
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult AddCompanyEntity([FromForm] CompanyAdd view)
        {
            string pathString = "Storage/profileDefault.jpg";

            if (view.Photo is not null)
            {
                _companyRepository.UploadArchive(view.Photo, view.CNPJ);
            }


            _companyRepository.Add(new CompanyModel(
                Name: view.Name,
                Location: view.Location,
                CNPJ: view.CNPJ,
                Photo: pathString,
                Email: view.Email,
                Password: view.Password,
                Phone: view.Phone,
                AvaliableAgenda: true
                ));
            

            return Ok();
        }


        [HttpDelete]
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
