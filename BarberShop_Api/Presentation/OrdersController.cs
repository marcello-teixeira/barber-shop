using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.ViewModel.OrdersViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Domain.Repositories;
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

        [HttpGet]
        public IActionResult GetAllOrdersCustomer()
        {
            var orders = _ordersRepository.Get();
            var claims = TokenService.GetClaims();

            string id = claims.First(item => item.Type == "Id").Value;

            List<OrdersModel> ordersMatchCompany = new();

            foreach (var order in orders)
            {
                if (order.CompanyID.ToString() == id)
                {
                    ordersMatchCompany.Add(order);
                }
                if (order.CustomerID.ToString() == id)
                { 
                    ordersMatchCompany.Add(order);
                }
            }

            return Ok(orders);
        }

        [HttpPost("post")]
        public IActionResult AddNewOrder(OrdersViewPost view)
        {

            _ordersRepository.Add(new OrdersModel(
                CustomerID: view.CustomerID,
                CustomerName: view.CustomerName,
                CustomerPhone: view.CustomerPhone,
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

    }
}
