using System;
using EventAPI.Controllers;
using EventAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using EventAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace TestProject
{
    [TestClass]
    public class EventControllerTest : ControllerTest
    {
        public EventControllerTest()
            : base(
                  new DbContextOptionsBuilder<EventContext>()
                  .UseInMemoryDatabase(databaseName: "EventListDb")
                  .Options)
        {
        }

        //testing using InMemoryDatabase du to integration with EF Core. 

        //Mock - controller hard coded to work with EventContext. Could probable be solved with dependency injection (interface)
        //


        [TestMethod]
        public void GetAllEvents_ReturnCounts_Expect3()
        {
            //arrange is done in ControllerTest.cs


            //act
            using (var context = new EventContext(ContextOptions))
            {
                var controller = new EventsController(context);

                var events = controller.GetEvents().Result;

                //assert
                Assert.AreEqual(3, events.Value.Count());

            }

        }
        [TestMethod]
        [DataRow(1, "Ulla")]
        [DataRow(2, "Niels")]
        public void GetEventsById_returnEvents_ExpectsNamesAsEqual(int id, string name)
        {
            //act
            using (var context = new EventContext(ContextOptions))
            {
                var controller = new EventsController(context);

                var _event = controller.GetEvent(id).Result;

                //assert
                Assert.AreEqual(_event.Value.Title, name);
            }
        }


        [TestMethod]
        [DataRow(4,"Lars")]
        public void PostEvent_returnSuccess(int id, string name)
        {
            Event newEvent = new Event { Id = id, Title = name };
            using (var context = new EventContext(ContextOptions))
            {
                //act
                var controller = new EventsController(context);

                var actionResult = controller.PostEvent(newEvent).Result.Result;
                var countEvents = controller.GetEvents().Result.Value.Count();

                //assert
                Assert.IsInstanceOfType(actionResult, typeof(CreatedAtActionResult));
                Assert.AreEqual(4, countEvents);

            }

        }



    }
}
