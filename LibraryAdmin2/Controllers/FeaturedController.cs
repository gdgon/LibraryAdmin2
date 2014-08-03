using LibraryAdmin2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAdmin2.Controllers
{
    public class FeaturedController : Controller
    {
        private LibraryAdmin2Db db = new LibraryAdmin2Db();

        public ActionResult TopBorrower()
        {
            var grouped = db.LogEvents.Where(e => e.Event == LogEvent.EventTypes.RequestApproved)
                                      .GroupBy(e => e.BorrowerId)
                                      .OrderByDescending(e => e.Count());

            if (grouped.Count() > 0)
            {
                var topGroup = grouped.First();
                var id = topGroup.First().BorrowerId;

                ViewBag.Name = db.Borrowers.Find(id).Name;
                ViewBag.Num = topGroup.Count();
            }
            return PartialView();
        }

        public ActionResult TopBook()
        {
            Book topBook = null;
            var grouped = db.LogEvents.Where(e => e.Event == LogEvent.EventTypes.RequestApproved)
                                     .GroupBy(e => e.BookId)
                                     .OrderByDescending(e => e.Count());
            if (grouped.Count() > 0)
            {
                var topGroup = grouped.First();
                var id = topGroup.First().BookId;
                topBook = db.Books.Find(id);
                ViewBag.Num = topGroup.Count();
            }
            return PartialView(topBook);
        }

        public ActionResult TopAuthor()
        {
            /*1: Get all authors
             *   for eachauthor, get all books
             *     for each book, count checkouts, add to author counter
             * 
             * */
            Author topAuthor = null;
            int topCount = 0;

            var authors = db.Authors.ToList();
            if (authors.Count() > 0)
            {
                foreach (var author in authors)
                {
                    int count = 0;
                    var books = author.Books;

                    foreach (var book in books)
                    {
                        count += db.LogEvents.Where(e => e.Event == LogEvent.EventTypes.RequestApproved)
                                             .Where(e => e.BookId == book.Id)
                                             .Count();
                    }

                    if (count > topCount)
                    {
                        topAuthor = author;
                        topCount = count;
                    }
                }
            }

            ViewBag.Num = topCount;
            return PartialView(topAuthor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}