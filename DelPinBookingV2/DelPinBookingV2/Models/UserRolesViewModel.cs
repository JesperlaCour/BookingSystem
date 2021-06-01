using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelPinBookingV2.Models;

namespace DelPinBookingV2.Models
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phonenumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SearchString { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
