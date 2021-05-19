using System;
namespace DelPinBooking.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public string CustomerId { get; set; }
        public bool AllDay { get; set; }
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int AddressId { get; set; }

        public Event()
        {
        }
    }
}

