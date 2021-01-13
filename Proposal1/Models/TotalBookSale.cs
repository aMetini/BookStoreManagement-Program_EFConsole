using System;
using System.Collections.Generic;

#nullable disable

namespace Proposal1
{
    public partial class TotalBookSale
    {
        public long Isbn { get; set; }
        public decimal? GrossBookSales { get; set; }
        public decimal? NetSales { get; set; }
    }
}
