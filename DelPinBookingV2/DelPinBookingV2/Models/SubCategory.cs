﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DelPinBookingV2.Models
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        public string SubCategoryName { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Resource> Recources { get; set; }
    }
}
