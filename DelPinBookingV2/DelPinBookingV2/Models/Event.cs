﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DelPinBookingV2.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public string UserName { get; set; }
        public bool AllDay { get; set; } = false;
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
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

