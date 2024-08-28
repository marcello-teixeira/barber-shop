using BarberShop_Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BarberShop_Api.Infrastructure
{
    public class ConnectionContext : DbContext
    {
        // Entities of th each models
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<OrdersModel> Orders { get; set; }
        public DbSet<HaircutModel> Haircuts { get; set; }

        // Dependecy Injection did in Program.cs
        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
        {
        }



    }
}
