using AutoMapper;

namespace BarberShop_Api.Application.DataTransfer
{
    public class CompanyDataTransfer 
    {
        public int Id { get; private set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string Photo { get; set; }
        public string? Phone { get; set; }
    }
}
