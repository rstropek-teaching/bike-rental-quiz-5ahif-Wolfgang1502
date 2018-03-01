using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class Customer
    {

        [Key]
        public int ID { get; set; }

        public Gender Gender { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(75)]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        [Required]
        [MaxLength(75)]
        public string Street { get; set; }

        [MaxLength(10)]
        public string HouseNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(75)]
        public string Town { get; set; }

    }

    public enum Gender
    {
        Male,
        Female,
        Unkown
    }

}
