namespace BarberShop_Api.Application.DataTransfer
{
    public class OrdersDataTransfer
    {
        public int Id { get;  set; }
        public required string CustomerName { get; set; }
        public required string CustomerPhone { get; set; }
        public required string CompanyName { get; set; }
        public required string CompanyPhone { get; set; }
        public required string CompanyLocation { get; set; }
        public required string HaircutName { get; set; }
        public decimal HaircutCost { get; set; }
        public DateTime HaircutDate { get; set; }
        public bool HaircutDone { get; set; }
    }
}
