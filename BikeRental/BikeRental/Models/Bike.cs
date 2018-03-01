using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class Bike
    {

        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(25)]
        public string Brand { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime PurchaseDate { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        [Column(TypeName = "date")]
        public DateTime LastService { get; set; }

        [Required]
        [Range(0.00d, Double.MaxValue)]
        public double PriceFirstHour { get; set; }

        [Required]
        [Range(1.00d, Double.MaxValue)]
        public double PriceAdditionalHour { get; set; }

        public Category Category { get; set; }

    }

    public enum Category
    {
        StandardBike,
        MountainBike,
        TreckingBike,
        RacingBike
    }

}
