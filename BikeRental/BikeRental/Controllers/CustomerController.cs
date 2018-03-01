using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Contexts;
using BikeRental.Models;

namespace BikeRental.Controllers
{
    [Route("api")]
    public class CustomerController : Controller
    {

        BikeRentalContext db = new BikeRentalContext();

        // Get all customers
        [HttpGet]
        [Route("getCustomers")]
        public IActionResult GetCustomers([FromQuery]string lastName)
        {
            var res = db.Customers.Where(p => (lastName == null || p.LastName.Contains(lastName)));
            return Ok(res);
        }

        // Create a new customer
        [HttpPost]
        [Route("addCustomer")]
        public IActionResult AddCustomer([FromBody]Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return Ok("Customer added successfully");
                }
                else
                {
                    return BadRequest("Error - Customer not created");
                }
            }
            catch(Exception e)
            {
                return BadRequest("Error - Customer not created");
            }

        }

        // Update a customer
        [HttpPut]
        [Route("updateCustomer/{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody]Customer newCustomer)
        {

            try
            {
                Customer customer = db.Customers.Where(p => id == p.ID).ToArray().Single();

                customer.Gender = newCustomer.Gender;
                customer.FirstName = newCustomer.FirstName;
                customer.LastName = newCustomer.LastName;
                customer.Birthday = newCustomer.Birthday;
                customer.Street = newCustomer.Street;
                customer.HouseNumber = newCustomer.HouseNumber;
                customer.ZipCode = newCustomer.ZipCode;
                customer.Town = newCustomer.Town;

                db.Customers.Update(customer);
                db.SaveChanges();
                return Ok("Customer " + customer.LastName + " updated successfully");
            }
            catch(Exception e)
            {
                return NotFound("Customer not updated");
            }
            
        }

        // Delete a customer
        [HttpDelete]
        [Route("deleteCustomer/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                Customer customer = db.Customers.Where(p => p.ID == id).ToArray().Single();
                if(customer == null)
                {
                    return NotFound("Customer not Found");
                }
                db.Customers.Remove(customer);
            }
            catch(Exception e)
            {
                return NotFound("Customer not Found");
            }
            
            db.SaveChanges();
            return Ok("Customer deleted successfully");
        }

        // Get all rentals for a specific customer
        [HttpGet]
        [Route("getRentals")]
        public IActionResult GetRentals([FromQuery]int customerId)
        {

            var res = db.Rentals.Where(p => p.Customer.ID == customerId);
            return Ok(res);

        }

    }
}
