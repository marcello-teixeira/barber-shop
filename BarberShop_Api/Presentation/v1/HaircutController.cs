using AutoMapper;
using BarberShop_Api.Application.DataTransfer;
using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.ViewModel.HaircutViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace BarberShop_Api.Presentation.v1
{
    [Route("/v{version:ApiVersion}/haircut/")]
    [ApiController]
    public class HaircutController : ControllerBase
    {

        private readonly IRepository<HaircutModel> _haircutRepository;
        private readonly IMapper _mapper;

        public HaircutController(IRepository<HaircutModel> haircutRepository, IMapper mapper)
        {
            _haircutRepository = haircutRepository ?? throw new ArgumentNullException(nameof(haircutRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize]
        [HttpGet("customers/{id}")]
        public IActionResult GetAllHaircutsToCustomer(int id)
        {
            var haircuts = _haircutRepository.Get(id, "CompanyID");
            List<HaircutDataTransfer> haircutDataTransfer = new();

            foreach (var haircut in haircuts)
            {
                haircutDataTransfer.Add(_mapper.Map<HaircutDataTransfer>(haircut));
            }

            return Ok(haircutDataTransfer);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Addhaircut(HaircutAdd view)
        {
            var companyId = TokenService.GetClaims().First(claim => claim.Type == "Id").Value;


            _haircutRepository.Add(new HaircutModel(
                Name: view.Name,
                Cost: view.Cost,
                CompanyID: Convert.ToInt32(companyId)));

            return Ok();
        }
    }
}
