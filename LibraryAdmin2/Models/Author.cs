using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryAdmin2.Models
{
    public class Author
    {
        public Author()
        {

        }

        public int Id { get; set; }
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Image")]
        [UIHint("Picture")]
        public string ImageUrl { get; set; }
        [Display(Name = "Short Description")]
        [UIHint("TextArea")]
        public string ShortDescription { get; set; }
        [UIHint("TextArea")]
        public string Description { get; set; }
        [UIHint("Authors")]
        public virtual ICollection<Book> Books { get; set; }
        
        public static bool Create(Author author, LibraryAdmin2Db db)
        {
            db.Authors.Add(author);
            db.SaveChanges();
            return true;
        }

        public static bool Edit(Author author, LibraryAdmin2Db db)
        {
            db.Entry(author).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public static bool Delete(Author author, LibraryAdmin2Db db)
        {
            db.Authors.Remove(author);
            db.SaveChanges();
            return true;
        }
    }
}