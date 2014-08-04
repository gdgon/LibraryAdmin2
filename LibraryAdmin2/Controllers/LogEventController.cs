using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryAdmin2.Models;
using LibraryAdmin2.Utils;

namespace LibraryAdmin2.Controllers
{
    [Authorize]
    public class LogEventController : Controller
    {
        private LibraryAdmin2Db db = new LibraryAdmin2Db();

        // GET: LogEvents
        public ActionResult Index()
        {
            return View(db.LogEvents.ToList());
        }

        // GET: LogEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogEvent logEvent = db.LogEvents.Find(id);
            if (logEvent == null)
            {
                return HttpNotFound();
            }

            if (logEvent.BorrowerId > 0)
            {
                var borrower = db.Borrowers.Find(logEvent.BorrowerId);
                ViewBag.BorrowerId = borrower.Id;
                ViewBag.BorrowerText = borrower.Name;
            }

            if (logEvent.BookId > 0)
            {
                var book = db.Books.Find(logEvent.BookId);
                ViewBag.BookId = book.Id;
                ViewBag.BookText = book.Title;
            }

            if (logEvent.AuthorId > 0)
            {
                var author = db.Authors.Find(logEvent.AuthorId);
                ViewBag.AuthorId = author.Id;
                ViewBag.AuthorText = author.Name;
            }

            if (logEvent.CheckoutId > 0)
            {
                var checkout = db.Checkouts.Find(logEvent.CheckoutId);
                ViewBag.CheckoutId = checkout.Id;
                ViewBag.CheckoutText = checkout.Id.ToString();
            }

            if (logEvent.RequestId > 0)
            {
                var request = db.CheckoutRequests.Find(logEvent.RequestId);
                ViewBag.RequestId = request.Id;
                ViewBag.RequestText = request.Id.ToString();
            }

            if (logEvent.PolicyId > 0)
            {
                var policy = db.Policies.Find(logEvent.PolicyId);
                ViewBag.PolicyId = policy.Id;
                ViewBag.PolicyText = policy.Name;
            }
            return View(logEvent);
        }

        // Get /LogEvent/Relevant/5
        public ActionResult Relevant(string RecordType, int id)
        {
            List<LogEvent> events = new List<LogEvent>();

            switch (RecordType)
            {
                case "Book":
                    var book = db.Books.Find(id);
                    events = db.LogEvents.Where(e => e.BookId == book.Id).ToList();
                    break;
                case "Author":
                    var author = db.Authors.Find(id);
                    events = db.LogEvents.Where(e => e.AuthorId == author.Id).ToList();
                    break;
                case "Borrower":
                    var borrower = db.Borrowers.Find(id);
                    events = db.LogEvents.Where(e => e.BorrowerId == borrower.Id).ToList();
                    break;
                case "Policy":
                    var policy = db.Policies.Find(id);
                    events = db.LogEvents.Where(e => e.PolicyId == policy.Id).ToList();
                    break;
                default:
                    break;
            }

            return PartialView("List", events);
        }

        // GET: /LogEvent/List
        // Prints out a list of items with an action link that directs to
        // another action with the Id of the selected items.
        // Optionally lists only items specified in ids.

        public ActionResult List(string ListAction,
                                 string ListLabel,
                                 int[] ids,
                                 string ListLabelClass,
                                 bool partial = false)
        {
            ViewBag.Db = db;
            if (ListAction != null)
            {
                ViewBag.ListAction = ListAction;

                if (ListLabel != null)
                    ViewBag.ListLabel = ListLabel;
                else
                    ViewBag.ListLabel = "Select";

                if (ListLabelClass != null)
                    ViewBag.ListLabelClass = ListLabelClass;

            }

            if (ids != null)
            {
                // List specified subset 
                List<LogEvent> events = db.LogEvents.Where(e => ids.Contains(e.Id))
                                                    .ToList();
                if (partial == true)
                    return PartialView(events);
                else
                    return View(events);
            }
            else
            {
                // List everything
                var events = db.LogEvents.ToList();
                if (events != null)
                {
                    if (partial == true)
                        return PartialView(events);
                    else
                        return View(events);
                }
                else
                    return View();
            }
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
