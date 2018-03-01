using BikeRental.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Contexts
{
    public class BikeRentalContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Bike> Bikes { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           //        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BikeRentalDatabase;Trusted_Connection=True");
            // Server=tcp:bikerentalpublishbauerdbserver.database.windows.net,1433;Initial Catalog=BikeRentalPublishBauer_db;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
            optionsBuilder.UseSqlServer("Server=bikerentalpublishbauerdbserver.database.windows.net;Database=BikeRentalPublishBauer_db;User ID=Bauer;Password=BikeRental1234;Trusted_Connection=False");
        }

    }
}
