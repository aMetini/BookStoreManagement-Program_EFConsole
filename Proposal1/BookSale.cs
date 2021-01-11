using System;
using System.Collections.Generic;

#nullable disable

namespace Proposal1
{
    public partial class BookSale
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public long Isbn { get; set; }
        public decimal? SalePrice { get; set; }
        public int? StoreId { get; set; }

        public virtual Book IsbnNavigation { get; set; }
        public virtual Order Order { get; set; }
        public virtual Store Store { get; set; }
    }
}
