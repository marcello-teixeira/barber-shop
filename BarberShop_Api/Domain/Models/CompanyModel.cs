using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop_Api.Domain.Models
{
    [Table("Company")]
    public class CompanyModel
    {
        public CompanyModel(int Id, string Name, string Location, string Login, string CNPJ, string Photo, string Email, string Password, string Phone, bool ActiveAgender)
        {
            this.Id = Id;
            this.Name = Name;
            this.Location = Location;
            this.Login = Login;
            this.CNPJ = CNPJ;
            this.Photo = Photo;
            this.Email = Email;
            this.Password = Password;
            this.Phone = Phone;
            this.ActiveAgender = ActiveAgender;
        }
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Location { get; set; }
        public string Login { get; private set; }
        public string CNPJ { get; private set; }
        public string Photo { get; private set; }
        public string Email { get; private set; }
        public string? Phone { get; private set; }
        public string Password { get; private set; }
        public bool ActiveAgender { get; private set; }
    }
}
