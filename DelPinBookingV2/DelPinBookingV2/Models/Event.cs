using System.ComponentModel.DataAnnotations;

namespace DelPinBookingV2.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Indtast gyldigt ressourceId")]
        public int ResourceId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email er ikke gyldig format")]
        public string UserName { get; set; }

        public bool AllDay { get; set; } = false;

        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Title skal være mellem 3 og 60 tegn")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        public string Start { get; set; }

        [Required]
        public string End { get; set; }

        [Required]
        [DataType(DataType.Text)]

        [MinLength(3,ErrorMessage = "Addressen er ikke gyldig format")]
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

