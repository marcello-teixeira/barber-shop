namespace BarberShop_Api.Application.ViewModel.CustomerViewModel
{
    public class CustomerViewPost
    {
        public required string Name { get; set; }
        public required string CPF { get; set; }
        public IFormFile? Photo { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Phone { get; set; }
    }
}
