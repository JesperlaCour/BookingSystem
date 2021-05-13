using Microsoft.EntityFrameworkCore;
using ResourceAPI.Models;

namespace ResourceAPI.Data
{
    public class ResourceContext : DbContext
    {


        public ResourceContext(DbContextOptions<ResourceContext> options)
            : base(options)
        {

        }

    public DbSet<Resource> Resources { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<SubCategory> SubCategories { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Server=tcp:lacour.database.windows.net,1433;Initial Catalog=DelPinResource;Persist Security Info=False;User ID=Jesper_laCour;Password=Azure1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }

    }
}
