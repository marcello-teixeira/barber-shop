using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop_Api.Domain.Models
{
    [Table("Clients")]
    public class ClientModel
    {
        public ClientModel(int Id, string Login_name, string CPF, string Photo, string Email, string Login_Password, string Phone)
        {
            this.Id = Id;
            this.Login_Name = Login_name;
            this.CPF = CPF;
            this.Photo = Photo;
            this.Email = Email;
            this.Login_Password = Login_Password;
            this.Phone = Phone;
        }
        [Key]
        [Column("id", TypeName = "int")]
        public int Id { get; private set; }

        [Column("login_name", TypeName = "varchar(max)")]
        public string Login_Name { get; private set; }

        [Column("cpf", TypeName = "varchar(max)")]
        public string CPF { get; private set; }

        [Column("photo", TypeName = "varchar(max)")]
        public string Photo { get; private set; }

        [Column("email", TypeName = "varchar(max)")]
        public string Email { get; private set; }

        [Column("login_password", TypeName = "varchar(max)")]
        public string Login_Password { get; private set; }

        [Column("phone", TypeName = "varchar(max)")]
        public string Phone { get; private set; }

    }
}
