using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop_Api.Domain.Models
{
    [Table("Orders")]
    public class OrdersModel
    {
        public OrdersModel(int CustomerID, string CustomerName, string CustomerPhone, int CompanyID, string CompanyName, string CompanyPhone, string CompanyLocation, 
                           int HaircutID, string HaircutName, decimal HaircutCost, DateTime HaircutDate, bool HaircutDone)
        {
            this.CustomerID = CustomerID;
            this.CustomerName = CustomerName;
            this.CustomerPhone = CustomerPhone;
            this.CompanyID = CompanyID;
            this.CompanyName = CompanyName;
            this.CompanyPhone = CompanyPhone;
            this.CompanyLocation = CompanyLocation;
            this.HaircutID = HaircutID;
            this.HaircutName = HaircutName;
            this.HaircutCost = HaircutCost;
            this.HaircutDate = HaircutDate;
            this.HaircutDone = HaircutDone;
        }

        public int Id { get; private set; }
        public int CustomerID { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerPhone { get; private set; }
        public int CompanyID { get; private set; }
        public string CompanyName { get; private set; }
        public string CompanyPhone { get; private set; }
        public string CompanyLocation { get; private set; }
        public int HaircutID { get; private set; }
        public string HaircutName { get; private set; }
        public decimal  HaircutCost { get; private set; }
        public DateTime HaircutDate { get; private set; }
        public bool HaircutDone { get; private set; }


    }
}
