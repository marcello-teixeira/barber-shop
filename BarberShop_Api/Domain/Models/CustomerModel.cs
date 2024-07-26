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
        public CustomerModel(int Id, string Name, string CPF, string Photo, string Email, string Password, string Phone)
        {
            this.Id = Id;
            this.Name = Name;
            this.CPF = CPF;
            this.Photo = Photo;
            this.Email = Email;
            this.Password = Password;
            this.Phone = Phone;
        }
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string CPF { get; private set; }
        public string Photo { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Phone { get; private set; }

    }
}
