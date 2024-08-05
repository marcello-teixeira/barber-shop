using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop_Api.Domain.Models
{
    [Table("Haircut")]
    public class HaircutModel
    {
        public HaircutModel(int Id, string HaircutName, decimal Cost, int CompanyID)
        {
            this.Id = Id;
            this.HaircutName = HaircutName;
            this.Cost = Cost;
            this.CompanyID = CompanyID;
        }

        [Key]
        public int Id { get; private set; }
        public string HaircutName { get; private set; }
        public decimal Cost  { get; private set; }
        public int CompanyID { get; private set; }
    }
}
