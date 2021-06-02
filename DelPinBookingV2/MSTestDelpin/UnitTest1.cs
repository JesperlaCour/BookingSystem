using System.Collections.Generic;
using DelPinBooking.Controllers;
using DelPinBookingV2.Controllers;
using DelPinBookingV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace MSTestDelpin

{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetEventFromIDTestType()
        {
            var calendar = new CalendarController("http://localhost:5001/api/");

            var result = calendar.GetEvent("2");

            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetResourcesFromID()
        {
            var resource = new ResourceController("http://localhost:5003/api/");

            var result = resource.GetCalendarResources();

           

            Assert.IsInstanceOfType(result, typeof(IEnumerable<JsonResult>));
        }
    }
}
