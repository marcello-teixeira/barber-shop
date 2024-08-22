using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.ViewModel.OrdersViewModel;
using BarberShop_Api.Domain.Models;
using BarberShop_Api.Application.DataTransfer;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BarberShop_Api.Presentation
{
    [Route("/orders/")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<OrdersModel> _ordersRepository;
        private readonly IRepository<CustomerModel> _customerRepository;
        private readonly IRepository<CompanyModel> _companyRepository;
        private readonly IMapper _mapper;

        public OrdersController(IRepository<OrdersModel> ordersRepository, IRepository<CustomerModel> customerRepository, IRepository<CompanyModel> companyRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var allOrders = _ordersRepository.Get();

            var claims = TokenService.GetClaims();
            int idClient = Convert.ToInt32(claims.First(claim => claim.Type == "Id").Value);

            List<OrdersDataTransfer> ordersDataTransfer = new();

            foreach (var order in allOrders)
            {
                if (order.CompanyID == idClient || order.CustomerID == idClient)
                {
                    ordersDataTransfer.Add(_mapper.Map<OrdersDataTransfer>(order));
                }
            }

            return Ok(ordersDataTransfer);
        }

        [Authorize]
        [HttpGet("date/{id}")]
        public IActionResult GetAllOrdersToCheckDateCustomer(int id)
        {
            var orders = _ordersRepository.Get(id, "CompanyID");
            List<DateTime> dateOrders = new();

            foreach(var order in orders)
            {
                dateOrders.Add(order.HaircutDate);
            }

            return Ok(dateOrders);
        }


        [Authorize]
        [HttpPost("new")]
        public IActionResult AddNewOrder(OrderAdd view)
        {

            var claims = TokenService.GetClaims();

            int id = Convert.ToInt32(claims.First(prop => prop.Type == "Id").Value);
            string name = claims.First(prop => prop.Type == "Name").Value;
            string phone = claims.First(prop => prop.Type == "Phone").Value;

            _ordersRepository.Add(new OrdersModel(
                CustomerID: id,
                CustomerName: name,
                CustomerPhone: phone,
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
