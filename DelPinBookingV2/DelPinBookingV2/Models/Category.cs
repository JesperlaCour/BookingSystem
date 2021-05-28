using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DelPinBookingV2.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        public string CategoryName { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
