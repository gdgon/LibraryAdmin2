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
            var topGroup = db.LogEvents.Where(e => e.Event == LogEvent.EventTypes.RequestApproved)
                                 .GroupBy(e => e.BorrowerId)
                                 .OrderByDescending(e => e.Count())
                                 .First();
            var id = topGroup.First().BorrowerId;

            ViewBag.Name = db.Borrowers.Find(id).Name;
            ViewBag.Num = topGroup.Count();
            return PartialView();
        }

        public ActionResult TopBook()
        {
            var topGroup = db.LogEvents.Where(e => e.Event == LogEvent.EventTypes.RequestApproved)
                                 .GroupBy(e => e.BookId)
                                 .OrderByDescending(e => e.Count())
                                 .First();
            var id = topGroup.First().BorrowerId;

            var topBook = db.Books.Find(id);
            ViewBag.Num = topGroup.Count();
            return PartialView(topBook);
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