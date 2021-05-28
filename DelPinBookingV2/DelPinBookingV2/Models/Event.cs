using System;
using System.ComponentModel.DataAnnotations;

namespace DelPinBookingV2.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public int ResourceId { get; set; }
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        public bool AllDay { get; set; } = false;
        [DataType(DataType.Text)]
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        [DataType(DataType.Text)]
        public string AddressStr { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Event()
        {
        }
    }
}

