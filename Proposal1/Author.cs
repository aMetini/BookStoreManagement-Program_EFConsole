using System;
using System.Collections.Generic;

#nullable disable

namespace Proposal1
{
    public partial class Author
    {
        public Author()
        {
            Isbnauthors = new HashSet<Isbnauthor>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }

        public virtual ICollection<Isbnauthor> Isbnauthors { get; set; }
    }
}
