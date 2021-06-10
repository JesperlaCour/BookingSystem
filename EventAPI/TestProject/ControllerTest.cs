using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventAPI.Data;
using EventAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace TestProject
{
    [TestClass]
    public class ControllerTest
    {
        public ControllerTest(DbContextOptions<EventContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<EventContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new EventContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Events.Add(new Event { Id = 1, ResourceId = 5, Start = "25-05-21", End = "26-05-21", Title = "Ulla" });
                context.Events.Add(new Event { Id = 2, ResourceId = 7, Start = "28-04-21", End = "30-04-21", Title = "Niels" });
                context.Events.Add(new Event { Id = 3, ResourceId = 2, Start = "20-05-21", End = "20-05-21", Title = "Frederik" });
                context.SaveChanges();
            }
        }
    }
}
