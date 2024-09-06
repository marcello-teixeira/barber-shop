using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace BarberShop_Api.Presentation.v1
{
    [ApiController]
    [Route("/v{version:ApiVersion}/geolocation/")]
    public class GeolocationController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> GeolocationReverse(Coords view)
        {
            ApiGeolocation apiGeolocation = new(view.Latitude, view.Longitude);

            try
            {
                await apiGeolocation.GetGeolocation();

                return Ok(apiGeolocation.Location);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
