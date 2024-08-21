namespace BarberShop_Api.Application.ViewModel.CompanyViewModel
{
    public class CompanyAdd
    {
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string Password { get; set; }
        public required string CNPJ { get; set; }
        public IFormFile? Photo { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
}
