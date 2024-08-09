using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop_Api.Domain.Models
{
    [Table("Company")]
    public class CompanyModel
    {
        public CompanyModel(string Name, string Location, bool AvaliableAgenda, string CNPJ, string Photo, 
            string Email, string Password, string Phone)
        {
            this.Name = Name;
            this.Location = Location;
            this.CNPJ = CNPJ;
            this.Photo = Photo;
            this.Email = Email;
            this.Password = Password;
            this.Phone = Phone;
            this.AvaliableAgenda = AvaliableAgenda;
        }
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Location { get; set; }
        public string CNPJ { get; private set; }
        public string Photo { get; private set; }
        public string Email { get; private set; }
        public string? Phone { get; private set; }
        public string Password { get; private set; }
        public bool AvaliableAgenda { get; private set; }
    }
}
