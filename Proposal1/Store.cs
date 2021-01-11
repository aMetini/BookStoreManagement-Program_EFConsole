using System;
using System.Collections.Generic;

#nullable disable

namespace Proposal1
{
    public partial class Store
    {
        public Store()
        {
            BookReturns = new HashSet<BookReturn>();
            BookSales = new HashSet<BookSale>();
            Orders = new HashSet<Order>();
            StockBalances = new HashSet<StockBalance>();
        }

        public int Id { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string StorePhoneNumber { get; set; }

        public virtual ICollection<BookReturn> BookReturns { get; set; }
        public virtual ICollection<BookSale> BookSales { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<StockBalance> StockBalances { get; set; }
    }
}
