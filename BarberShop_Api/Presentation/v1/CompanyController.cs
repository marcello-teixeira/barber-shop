using BarberShop_Api.Application.ViewModel;
using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.DataTransfer;
using BarberShop_Api.Application.ViewModel.CompanyViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Asp.Versioning;

namespace BarberShop_Api.Presentation.v1
{
    [Route("/v{version:ApiVersion}/company/")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IRepository<CompanyModel> _companyRepository;
        private readonly IMapper _mapper;

        public CompanyController(IRepository<CompanyModel> companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            // Variable that will make automapping
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllCompanies()
        {
            var companies = _companyRepository.Get();

            List<CompanyDataTransfer> companiesDataTransfer = new();

            foreach (var item in companies)
            {
                companiesDataTransfer.Add(_mapper.Map<CompanyDataTransfer>(item));
            }

            return Ok(companiesDataTransfer);
        }

        [Authorize]
        [HttpGet("own")]
        public IActionResult GetCompany()
        {
            var company = _companyRepository.GetByClaim();

            return Ok(company);
        }

        [Authorize]
        [HttpGet("get-photo")]
        public IActionResult GetPhotoCompany()
        {
            var company = _companyRepository.GetByClaim();
            byte[] bytePhoto;

            try
            {
                if (company == null)
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

        [Authorize]
        [HttpPatch("patch-photo")]
        public IActionResult PatchPhoto([FromForm] PatchPhoto view)
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
            catch (Exception e)
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

            string EncriptedPassword = EncryptCode.TransformCode256Hash(view.Password);

            _companyRepository.Add(new CompanyModel(
                Name: view.Name,
                Location: view.Location,
                CNPJ: view.CNPJ,
                Photo: pathString,
                Email: view.Email,
                Password: EncriptedPassword,
                Phone: view.Phone,
                AvaliableAgenda: true
                ));


            return Ok();
        }

        [Authorize]
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

        [HttpPost("verify-doc")]
        public IActionResult VerifyDoc(DocumentView view)
        {
            var companies = _companyRepository.Get();
            bool isAvaliable;

            foreach (var company in companies)
            {
                if (company.CNPJ == view.Document)
                {
                    return Ok(false);
                }
            }

            isAvaliable = VerifyDocument.Verify(view.Document);

            return Ok(isAvaliable);
        }


    }
}
