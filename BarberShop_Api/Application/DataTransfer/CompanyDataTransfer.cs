using AutoMapper;

namespace BarberShop_Api.Application.DataTransfer
{
    public class CompanyDataTransfer 
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Photo { get; set; }
        public string? Phone { get; set; }
        public bool AvaliableAgenda { get; set; }
    }
}
