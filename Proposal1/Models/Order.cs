using System;
using System.Collections.Generic;

#nullable disable

namespace Proposal1
{
    public partial class Order
    {
        public Order()
        {
            BookReturns = new HashSet<BookReturn>();
            BookSales = new HashSet<BookSale>();
        }

        public string Id { get; set; }
        public string CustomerId { get; set; }
        public int? BoutiqueId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }

        public virtual Store Boutique { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<BookReturn> BookReturns { get; set; }
        public virtual ICollection<BookSale> BookSales { get; set; }
    }
}
