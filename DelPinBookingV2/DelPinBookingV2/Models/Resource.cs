using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DelPinBookingV2.Models
{
    public class Resource
    {
       
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        public string Title { get; set; }
        public string groupId { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
