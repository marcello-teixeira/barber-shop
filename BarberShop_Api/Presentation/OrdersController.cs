using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.ViewModel.OrdersViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop_Api.Presentation
{
    [Route("/orders/")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<OrdersModel> _ordersRepository;

        public OrdersController(IRepository<OrdersModel> ordersRepository)
        {
            _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _ordersRepository.Get();
            var claims = TokenService.GetClaims();

            int id = Convert.ToInt32(claims.First(item => item.Type == "Id").Value);

            List<OrdersModel> ordersMatch = new();

            foreach (var order in orders)
            {
                if (order.CompanyID == id)
                {
                    ordersMatch.Add(order);
                }
                if (order.CustomerID == id)
                { 
                    ordersMatch.Add(order);
                }
            }

            return Ok(ordersMatch);
        }

        [Authorize]
        [HttpGet("date/{id}")]
        public IActionResult GetAllOrdersToCheckDateCustomer(int id)
        {
            List<OrdersModel> orders = _ordersRepository.Get(id, "CompanyID");          

            return Ok(orders);
        }


        [Authorize]
        [HttpPost("new")]
        public IActionResult AddNewOrder(OrderAdd view)
        {

            var claims = TokenService.GetClaims();

          
            _ordersRepository.Add(new OrdersModel(
                CustomerID: Convert.ToInt32(claims.First(prop => prop.Type == "Id" ).Value),
                CustomerName: claims.First(prop => prop.Type == "Name").Value,
                CustomerPhone: claims.First(prop => prop.Type == "Phone").Value,
                CompanyID: view.CompanyID,
                CompanyName: view.CompanyName,
                CompanyPhone: view.CompanyPhone,
                CompanyLocation: view.CompanyLocation,
                HaircutID: view.HaircutID,
                HaircutName: view.HaircutName,
                HaircutCost: view.HaircutCost,
                HaircutDate: view.HaircutDate,
                HaircutDone: false
                ));

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _ordersRepository.Delete(id);

            return Ok();
        }

        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult UpdateHaircutDone(int id)
        {
            _ordersRepository.Patch(id, true, "HaircutDone");

            return Ok();
        }

    }
}
