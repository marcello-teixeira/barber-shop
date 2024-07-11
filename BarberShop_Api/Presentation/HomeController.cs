using Microsoft.AspNetCore.Mvc;

namespace BarberShop_Api.Presentation
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
