using System;
using System.Collections.Generic;

namespace EventAPI.Model
{
    public class ZipCode
    {
        public int ZipCodeId { get; set; }
        public string City { get; set; }

        public ICollection<Address> Addresses { get; set; }

    }
}
