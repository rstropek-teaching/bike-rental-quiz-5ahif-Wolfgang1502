using BikeRental;
using BikeRental.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BikeRentalTest
{
    [TestClass]
    public class CalculationTest
    {

        CostCalculator calc = new CostCalculator();

        private Customer customer = new Customer()
        {
            Gender = 0,
            FirstName = "TestVorname",
            LastName = "TestNachname",
            Birthday = new DateTime(1998, 2, 15),
            Street = "Teststaﬂe",
            HouseNumber = "1",
            ZipCode = "4312",
            Town = "Ried"
        };

        private Bike bike = new Bike()
        {
            Brand = "KTM",
            PurchaseDate = new DateTime(2018, 2, 14),
            Notes = "TestNotes",
            LastService = new DateTime(2018, 2, 14),
            PriceFirstHour = 3,
            PriceAdditionalHour = 5,
            Category = 0
        };


        [TestMethod]
        public void MoreHours()
        {

            Rental rental = new Rental()
            {
                Customer = customer,
                Bike = bike,
                RentalBegin = new DateTime(2018, 2, 14, 8, 15, 0),
                RentalEnd = new DateTime(2018, 2, 14, 10, 30, 0),
            };

            Assert.AreEqual(13, calc.Calculate(rental, bike));

        }

        [TestMethod]
        public void FirstHour()
        {

            Rental rental = new Rental()
            {
                Customer = customer,
                Bike = bike,
                RentalBegin = new DateTime(2018, 2, 14, 8, 15, 0),
                RentalEnd = new DateTime(2018, 2, 14, 8, 45, 0),
            };

            Assert.AreEqual(3, calc.Calculate(rental, bike));

        }

        [TestMethod]
        public void Under15Minutes()
        {

            Rental rental = new Rental()
            {
                Customer = customer,
                Bike = bike,
                RentalBegin = new DateTime(2018, 2, 14, 8, 15, 0),
                RentalEnd = new DateTime(2018, 2, 14, 8, 25, 0),
            };

            Assert.AreEqual(0, calc.Calculate(rental, bike));

        }

    }
}
