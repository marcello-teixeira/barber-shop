using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop_Api.Domain.Models
{
    [Table("Business")]
    public class BusinessModel
    {
        public BusinessModel(int Id, string BusinessName, string Location, string LoginName, string CNPJ, string Photo, string Email, string LoginPassword, string Phone, bool ActiveAgender)
        {
            this.Id = Id;
            this.BusinessName = BusinessName;
            this.Location = Location;
            this.LoginName = LoginName;
            this.CNPJ = CNPJ;
            this.Photo = Photo;
            this.Email = Email;
            this.LoginPassword = LoginPassword;
            this.Phone = Phone;
            this.ActiveAgender = ActiveAgender;
        }
        [Key]
        public int Id { get; private set; }
        public string BusinessName { get; private set; }
        public string Location { get; set; }
        public string LoginName { get; private set; }
        public string CNPJ { get; private set; }
        public string Photo { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string LoginPassword { get; private set; }
        public bool ActiveAgender { get; private set; }
    }
}
