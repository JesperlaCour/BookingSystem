using System;
using System.Collections.Generic;

namespace EventAPI.Model
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }

        public int ZipCodeId { get; set; }
        public ZipCode ZipCode { get; set; }

        public ICollection<Event> Events { get; set; }




    }
}
