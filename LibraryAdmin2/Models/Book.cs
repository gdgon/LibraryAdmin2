using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryAdmin2.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "ISBN")]
        public string Isbn { get; set; }
        [Display(Name = "Image")]
        [UIHint("Picture")]
        public string ImageUrl { get; set; }
        [Display(Name = "Short Description")]
        [UIHint("TextArea")]
        public string ShortDescription { get; set; }
        [Display(Name = "Description")]
        [UIHint("TextArea")]
        public string Description { get; set; }
        [Display(Name = "Available Copies")]
        [Range(0, Int32.MaxValue)]
        public int AvailableCopies { get; set; }
        [Display(Name = "Authors")]
        [UIHint("Authors")]
        public virtual ICollection<Author> Authors { get; set; }

        public static bool Create(Book book, LibraryAdmin2Db db)
        {
            db.Books.Add(book);
            db.SaveChanges();
            LogEvent.BookCreate(book.Id, db);
            return true;
        }

        public static bool Edit(Book book, LibraryAdmin2Db db)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            LogEvent.BookEdit(book.Id, db);
            return true;
        }

        public static bool Delete(Book book, LibraryAdmin2Db db)
        {
            db.Books.Remove(book);
            db.SaveChanges();
            LogEvent.BookDelete(book.Id, db);
            return true;
        }
    }
}