namespace BarberShop_Api.Application.DataTransfer
{
    public class CustomerDataTransfer
    {
        public int Id { get;  set; }
        public required string Name { get;  set; }
        public required string Phone { get;  set; }
    }
}
