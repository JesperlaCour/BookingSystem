using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DelPinBookingV2.Models
{
    public class Resource
    {
       

        public int Id { get; set; }
        public string Title { get; set; }
        public string groupId { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
