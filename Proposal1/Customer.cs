using System;
using System.Collections.Generic;

#nullable disable

namespace Proposal1
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public bool? BookClubMember { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
