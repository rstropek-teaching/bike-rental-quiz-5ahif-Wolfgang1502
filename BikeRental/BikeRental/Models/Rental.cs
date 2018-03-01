using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class Rental
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public Bike Bike { get; set; }

        [Required]
        public DateTime RentalBegin { get; set; }

        [DateGreaterThan("RentalBegin")]
        public DateTime? RentalEnd { get; set; }

        [Range(1.00d, Double.MaxValue)]
        public double TotalCosts { get; set; }

        public Boolean Paid { get; set; }

    }
}
