using System;
using System.ComponentModel.DataAnnotations;

namespace BikeRental.Models
{

    [AttributeUsage(AttributeTargets.Property)]
    public class DateGreaterThan : ValidationAttribute
    {

        private string RentalBegin { get; set; }

        public DateGreaterThan(string rentalBegin)
        {
            RentalBegin = rentalBegin;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            DateTime rentalBegin = (DateTime)validationContext.ObjectType.GetProperty(RentalBegin).GetValue(validationContext.ObjectInstance, null);
            DateTime rentalEnd = (DateTime)value;

            if (rentalBegin < rentalEnd)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("RentalBegin must be before RentalEnd");
        }

    }
}
