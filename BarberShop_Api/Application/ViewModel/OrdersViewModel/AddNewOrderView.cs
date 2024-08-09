namespace BarberShop_Api.Application.ViewModel.OrdersViewModel
{
    public class AddNewOrderView
    {
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
