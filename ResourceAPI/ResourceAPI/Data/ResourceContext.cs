using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResourceAPI.Models;

namespace ResourceAPI.Data
{
    public class ResourceContext : DbContext
    {
        
            public DbSet<Resource> Resources { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<SubCategory> SubCategories { get; set; }

    }
}
