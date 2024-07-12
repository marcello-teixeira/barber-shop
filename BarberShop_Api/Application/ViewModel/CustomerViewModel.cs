namespace BarberShop_Api.Application.ViewModel
{
    public class CustomerViewModel
    {
        public required int Id { get;  set; }
        public required string LoginName { get;  set; }
        public required string CPF { get;  set; }
        public string? Photo { get;  set; }
        public required string Email { get;  set; }
        public required string LoginPassword { get;  set; }
        public required string Phone { get;  set; }
    }
}
