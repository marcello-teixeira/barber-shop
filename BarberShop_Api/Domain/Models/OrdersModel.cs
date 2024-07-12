using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop_Api.Domain.Models
{
    [Table("Ordens")]
    public class OrdersModel
    {
        public OrdersModel(int Id, string CustomerName, string CustomerPhone, string BusinessName, string BusinessPhone, string BusinessLocation, 
                           string HaircutName, decimal HaircutCost, DateTime HaircutDate, bool haircutDone)
        {
            this.Id = Id;
            this.CustomerName = CustomerName;
            this.CustomerPhone = CustomerPhone;
            this.BusinessName = BusinessName;
            this.BusinessPhone = BusinessPhone;
            this.BusinessLocation = BusinessLocation;
            this.HaircutName = HaircutName;
            this.HaircutCost = HaircutCost;
            this.HaircutDate = HaircutDate;
            this.HaircutDone = haircutDone;
        }

        public int Id { get; private set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; private set; }
        public string CustomerPhone { get; private set; }
        public string BusinessName { get; private set; }
        public string BusinessPhone { get; private set; }
        public string BusinessLocation { get; private set; }
        public string HaircutName { get; private set; }
        public decimal  HaircutCost { get; private set; }
        public DateTime HaircutDate { get; private set; }
        public bool HaircutDone { get; private set; }


    }
}
