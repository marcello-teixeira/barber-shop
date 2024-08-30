namespace BarberShop_Api.Application.DataTransfer
{
    public class HaircutDataTransfer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Cost { get; set; }
    }
}
