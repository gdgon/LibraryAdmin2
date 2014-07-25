using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LibraryAdmin2.Models
{
    public class BookAuthor
    {
        public int Id { get; set; }
        public virtual int AuthorId { get; set; }
        [UIHint("Author")]
        public virtual Author Author { get; set; }
        public virtual int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
