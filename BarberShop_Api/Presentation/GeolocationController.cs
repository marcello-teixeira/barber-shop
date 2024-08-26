using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BarberShop_Api.Presentation
{
    [ApiController]
    [Route("/geolocation/")]
    public class GeolocationController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> GeolocationReverse(Coords view)
        {
            ApiGeolocation apiGeolocation = new(view.Latitude, view.Longitude);
            
            try
            {
                await apiGeolocation.GetGeolocationAsync();

                return Ok(apiGeolocation.Geolocation);
            }
            catch (Exception ex)
            {
               return StatusCode(500, ex.Message);
            }
        }
    }
}
