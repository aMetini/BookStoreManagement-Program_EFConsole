using System;
using System.Collections.Generic;

#nullable disable

namespace Proposal1
{
    public partial class BookReturn
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int StoreId { get; set; }
        public long Isbn { get; set; }
        public decimal? AmountRefunded { get; set; }

        public virtual Book IsbnNavigation { get; set; }
        public virtual Order Order { get; set; }
        public virtual Store Store { get; set; }
    }
}
