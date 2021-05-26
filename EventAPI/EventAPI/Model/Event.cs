using System;
namespace EventAPI.Model
{
    public class Event
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public string UserName { get; set; }

        public bool AllDay { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Title { get; set; }
        public string AddressStr { get; set; }

    }
}
