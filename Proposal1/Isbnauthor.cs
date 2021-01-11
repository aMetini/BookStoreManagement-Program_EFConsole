using System;
using System.Collections.Generic;

#nullable disable

namespace Proposal1
{
    public partial class Isbnauthor
    {
        public long Isbn { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book IsbnNavigation { get; set; }
    }
}
