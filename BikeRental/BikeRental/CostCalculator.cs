using BikeRental.Models;
using System;

namespace BikeRental
{
    public class CostCalculator
    {

        public double Calculate(Rental rental, Bike bike)
        {

            if(bike == null)
            {
                return -1;
            }
            TimeSpan time = (DateTime)rental.RentalEnd - rental.RentalBegin;
            double minutes = time.TotalMinutes;
            double costs = 0;

            if(minutes <= 15)
            {
                costs = 0;
            }
            else
            {
                costs = costs + bike.PriceFirstHour;
                minutes = minutes - 60;

                while(minutes > 0)
                {
                    costs = costs + bike.PriceAdditionalHour;
                    minutes = minutes - 60;
                }
            }
            return costs;
        }

    }
}
