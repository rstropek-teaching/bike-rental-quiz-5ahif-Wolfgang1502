using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Models;
using BikeRental.Contexts;

namespace BikeRental.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class RentalController : Controller
    {

        BikeRentalContext db = new BikeRentalContext();

        // Start a rental
        [HttpGet]
        [Route("startRental")]
        public IActionResult StartRental([FromQuery]int customerId, int bikeId)
        {

            Customer customer;
            Bike bike;
            Rental rental;

            try
            {
                customer = db.Customers.Where(p => p.ID == customerId).ToArray().Single();
                bike = db.Bikes.Where(p => p.ID == bikeId).ToArray().Single();

                List<Rental> rentals = db.Rentals.Where(p => p.Customer.ID == customerId && p.Bike.ID == bikeId).ToList();
                foreach (Rental r in rentals)
                {
                    if (r.RentalEnd == null)
                    {
                        return BadRequest("Customer has an active Rental");
                    }
                }

                rental = new Rental { Customer = customer, Bike = bike, RentalBegin = DateTime.Now, Paid = false };
                db.Rentals.Add(rental);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest("Rental could not be startet");
            }

            return Ok(rental);
        }

        // End an existing, active rental
        [HttpGet]
        [Route("endRental")]
        public IActionResult EndRental([FromQuery]int customerId, int bikeId)
        {

            Rental rental = null;

            try
            {
                List<Rental> rentals = db.Rentals.Where(p => p.Customer.ID == customerId && p.Bike.ID == bikeId).ToList();
                foreach(Rental r in rentals) {
                    if(r.RentalEnd == null)
                    {
                        rental = r;
                    }
                }

                if (rental == null)
                {
                    return BadRequest("The customer has no active rentals");
                }

                Bike bike = db.Bikes.First(p => p.ID == bikeId);

                rental.RentalEnd = DateTime.Now;
                rental.TotalCosts = new CostCalculator().Calculate(rental, bike);

                db.Rentals.Update(rental);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest("Rental could not be ended");
            }

            return Ok(rental);
        }

        // Mark an ended rental as paid
        [HttpGet]
        [Route("markAsPaid")]
        public IActionResult MarkAsPaid([FromQuery]int rentalId)
        {

            Rental rental;

            try
            {
                rental = db.Rentals.Where(p => p.ID == rentalId).ToArray().Single();
                //if(rental.TotalCosts <= 0)
                //{
                //    return BadRequest("No Price");
                //}

                if(rental.TotalCosts == 0)
                {
                    return BadRequest("There are no costs");
                }
                else if(rental.RentalEnd == null)
                {
                    return BadRequest("Cannot pay active rentals");
                }
                else if(rental.Paid)
                {
                    return BadRequest("Rental already paid");
                }

                rental.Paid = true;

                db.Rentals.Update(rental);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest("Rental could not be paid");
            }

            return Ok("Paid");

        }

        // Get a list of unpaid, ended rentals with total price > 0
        [HttpGet]
        [Route("unpaidRentals")]
        public IActionResult UnpaidRentals()
        {

            var res = db.Rentals.Where(p => p.Paid == false && p.RentalEnd != null && p.TotalCosts > 0).SelectMany(p => new object[] { p.Customer.ID, p.Customer.FirstName, p.Customer.LastName, p.ID, p.RentalBegin, p.RentalEnd, p.TotalCosts });

            return Ok(res);

        }

    }
}
