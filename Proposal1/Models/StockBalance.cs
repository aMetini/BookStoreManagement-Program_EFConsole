using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Proposal1
{
    [Table("StockBalance")]

    public partial class StockBalance
    {
        
        public int BoutiqueId { get; set; }

        [Column("ISBN")]
        public long Isbn { get; set; }

        [Column("Number")]
        public int? Number { get; set; }

        public virtual Store Boutique { get; set; }
        public virtual Book IsbnNavigation { get; set; }
    }
}
