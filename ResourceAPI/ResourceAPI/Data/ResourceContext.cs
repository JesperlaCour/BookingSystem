using Microsoft.EntityFrameworkCore;
using ResourceAPI.Models;
using System.Configuration;

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

            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["Db"].ConnectionString);

            }
        }

    }
}
