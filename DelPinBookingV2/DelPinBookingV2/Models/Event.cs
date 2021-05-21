using System;

namespace DelPinBookingV2.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public string CustomerId { get; set; }
        public bool AllDay { get; set; } = false;
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int AddressId { get; set; }

        public Event()
        {
        }
    }
}

