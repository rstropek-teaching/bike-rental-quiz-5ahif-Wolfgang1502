using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Contexts;
using BikeRental.Models;

namespace BikeRental.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class BikeController : Controller
    {

        BikeRentalContext db = new BikeRentalContext();

        // Get all bikes that are currently available
        [HttpGet]
        [Route("getBikes")]
        public IActionResult GetBikes([FromQuery]string sorting)
        {
            if(sorting == null)
            {
                return Ok(db.Bikes);
            }
            if(sorting.Equals("FirstHour"))
            {
                return Ok(db.Bikes.OrderBy(p => p.PriceFirstHour));
            }
            else if(sorting.Equals("AdditionalHour"))
            {
                return Ok(db.Bikes.OrderBy(p => p.PriceAdditionalHour));
            }
            else if(sorting.Equals("PurchaseDate"))
            {
                return Ok(db.Bikes.OrderByDescending(p => p.PurchaseDate));
            }
            else
            {
                return BadRequest("Error");
            }
        }

        // Create a bike
        [HttpPost]
        [Route("addBike")]
        public IActionResult AddBike([FromBody]Bike bike)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Bikes.Add(bike);
                    db.SaveChanges();
                    return Ok("Bike added successfully");
                }
                else
                {
                    return BadRequest("Error - Bike not created");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error - Bike not created");
            }

        }

        // Update a bike
        [HttpPut]
        [Route("updateBike/{id}")]
        public IActionResult UpdateBike(int id, [FromBody]Bike newBike)
        {

            try
            {
                Bike bike = db.Bikes.Where(p => id == p.ID).ToArray().Single();

                bike.Brand = newBike.Brand;
                bike.PurchaseDate = newBike.PurchaseDate;
                bike.Notes = newBike.Notes;
                bike.LastService = newBike.LastService;
                bike.PriceFirstHour = newBike.PriceFirstHour;
                bike.PriceAdditionalHour = newBike.PriceAdditionalHour;
                bike.Category = newBike.Category;

                db.Bikes.Update(bike);
                db.SaveChanges();
                return Ok("Bike updated successfully");
            }
            catch (Exception e)
            {
                return NotFound("Bike not updated");
            }

        }

        // Delete a bike
        [HttpDelete]
        [Route("deleteBike/{id}")]
        public IActionResult DeleteBike(int id)
        {
            try
            {
                Bike bike = db.Bikes.Where(p => p.ID == id).ToArray().Single();
                if (bike == null)
                {
                    return NotFound("Bike not Found");
                }
                db.Bikes.Remove(bike);
            }
            catch (Exception e)
            {
                return NotFound("Bike not Found");
            }

            db.SaveChanges();
            return Ok("Bike deleted successfully");

        }
    }
}
