using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DelPinBookingV2.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
