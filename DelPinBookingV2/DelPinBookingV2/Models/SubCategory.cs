using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DelPinBookingV2.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Resource> Recources { get; set; }
    }
}
