using System;
using System.Collections.Generic;

#nullable disable

namespace Proposal1
{
    public partial class Book
    {
        public Book()
        {
            BookReturns = new HashSet<BookReturn>();
            BookSales = new HashSet<BookSale>();
            Isbnauthors = new HashSet<Isbnauthor>();
            StockBalances = new HashSet<StockBalance>();
        }

        public long Isbn13 { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public decimal Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Publisher { get; set; }

        public virtual ICollection<BookReturn> BookReturns { get; set; }
        public virtual ICollection<BookSale> BookSales { get; set; }
        public virtual ICollection<Isbnauthor> Isbnauthors { get; set; }
        public virtual ICollection<StockBalance> StockBalances { get; set; }
    }
}
