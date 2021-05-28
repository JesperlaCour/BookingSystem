using System.ComponentModel.DataAnnotations;

namespace DelPinBookingV2.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public int ResourceId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string UserName { get; set; }

        public bool AllDay { get; set; } = false;

        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Title is not valid")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        public string Start { get; set; }

        [Required]
        public string End { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string AddressStr { get; set; }

        public bool delivery { get; set; }
        public string deliveryComments { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Event()
        {
        }
    }
}

