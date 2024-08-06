namespace BarberShop_Api.Application.ViewModel.OrdersViewModel
{
    public class OrdersViewPost
    {
        public int CustomerID { get;  set; }
        public string CustomerName { get;  set; }
        public string CustomerPhone { get;  set; }
        public int CompanyID { get;  set; }
        public string CompanyName { get;  set; }
        public string CompanyPhone { get;  set; }
        public string CompanyLocation { get;  set; }
        public int HaircutID { get;  set; }
        public string HaircutName { get;  set; }
        public decimal HaircutCost { get;  set; }
        public DateTime HaircutDate { get;  set; }
    }
}
