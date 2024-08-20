namespace BarberShop_Api.Application.ViewModel.CustomerViewModel
{
    public class CustomerPatchPhoto
    {
        public int Id { get; set; }
        public IFormFile? Photo { get; set; } 
    }
}
