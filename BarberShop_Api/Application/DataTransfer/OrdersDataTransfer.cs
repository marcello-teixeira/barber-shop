namespace BarberShop_Api.Application.DataTransfer
{
    public class OrdersDataTransfer
    {
        public int Id { get;  set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyLocation { get; set; }
        public string HaircutName { get; set; }
        public decimal HaircutCost { get; set; }
        public DateTime HaircutDate { get; set; }
        public bool HaircutDone { get; set; }
    }
}
