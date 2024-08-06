using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.ViewModel.HaircutViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop_Api.Presentation
{
    [Route("/haircut/")]
    [ApiController]
    public class HaircutController : ControllerBase
    {

        private readonly IRepository<HaircutModel> _haircutRepository;

        public HaircutController(IRepository<HaircutModel> haircutRepository)
        {
            _haircutRepository = haircutRepository ?? throw new ArgumentNullException(nameof(haircutRepository));
        }


        [HttpGet]
        public IActionResult GetAllHaircuts()
        {
            List<HaircutModel> haircuts = _haircutRepository.Get();

            return Ok(haircuts);
        }

        [HttpPost]
        public IActionResult Addhaircut(HaircutViewPost view)
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
