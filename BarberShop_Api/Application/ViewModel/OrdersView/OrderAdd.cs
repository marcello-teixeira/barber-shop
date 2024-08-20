namespace BarberShop_Api.Application.ViewModel.OrdersViewModel
{
    public class OrderAdd
    {
        public int CompanyID { get;  set; }
        public required string CompanyName { get;  set; }
        public required string CompanyPhone { get;  set; }
        public required string CompanyLocation { get;  set; }
        public int HaircutID { get;  set; }
        public required string HaircutName { get;  set; }
        public decimal HaircutCost { get;  set; }
        public DateTime HaircutDate { get;  set; }
    }
}
