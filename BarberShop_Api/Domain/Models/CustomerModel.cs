using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop_Api.Domain.Models
{
    [Table("Customer")]
    public class CustomerModel
    {
        public CustomerModel()
        {
        }
        public CustomerModel(int Id, string LoginName, string CPF, string Photo, string Email, string LoginPassword, string Phone)
        {
            this.Id = Id;
            this.LoginName = LoginName;
            this.CPF = CPF;
            this.Photo = Photo;
            this.Email = Email;
            this.LoginPassword = LoginPassword;
            this.Phone = Phone;
        }
        [Key]
        public int Id { get; private set; }
        public string LoginName { get; private set; }
        public string CPF { get; private set; }
        public string Photo { get; private set; }
        public string Email { get; private set; }
        public string LoginPassword { get; private set; }
        public string Phone { get; private set; }

    }
}
