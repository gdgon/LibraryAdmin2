using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryAdmin2.Models;

namespace LibraryAdmin2.Controllers
{
    public class CheckoutController : Controller
    {
        private LibraryAdmin2Db db = new LibraryAdmin2Db();

        // GET: /Checkout/
        public ActionResult Index()
        {
            var checkouts = db.Checkouts.Include(c => c.Book).Include(c => c.Policy);
            return View(checkouts.ToList());
        }

        // GET: /Checkout/Borrow/5
        public ActionResult Borrow(int? id)
        {
            if (id == null)
                new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var book = db.Books.Find(id);
            if (book == null)
                return HttpNotFound();
            if (book.AvailableCopies < 1)
                return View("RequestNoCopiesAvailable");
            return View(book);
        }

        // POST: /Checkout/Borrow/5
        [HttpPost]
        public ActionResult Borrow(int? id, string FirstName, string LastName)
        {
            var book = db.Books.Find(id);
            var result = CheckoutRequest.Request(book, FirstName, LastName, db);

            if (result == CheckoutRequest.CreateRequestResult.Success)
                return View("RequestSuccess");
            else
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }



        // GET: /Checkout/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checkout checkout = db.Checkouts.Find(id);
            if (checkout == null)
            {
                return HttpNotFound();
            }
            return View(checkout);
        }

        // GET: /Checkout/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName");
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title");
            ViewBag.PolicyId = new SelectList(db.Policies, "Id", "Name");
            return View();
        }

        // POST: /Checkout/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookId,PolicyId")] Checkout checkout)
        {
            if (ModelState.IsValid)
            {
                checkout.DueDate = DateTime.Now;
                checkout.CheckoutDate = DateTime.Now;
                db.Checkouts.Add(checkout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", checkout.AuthorId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", checkout.BookId);
            ViewBag.PolicyId = new SelectList(db.Policies, "Id", "Name", checkout.PolicyId);
            return View(checkout);
        }

        // GET: /Checkout/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checkout checkout = db.Checkouts.Find(id);
            if (checkout == null)
            {
                return HttpNotFound();
            }
            //ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", checkout.AuthorId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", checkout.BookId);
            ViewBag.PolicyId = new SelectList(db.Policies, "Id", "Name", checkout.PolicyId);
            return View(checkout);
        }

        // POST: /Checkout/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookId,AuthorId,PolicyId,Returned,CheckoutDate,DueDate")] Checkout checkout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", checkout.AuthorId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", checkout.BookId);
            ViewBag.PolicyId = new SelectList(db.Policies, "Id", "Name", checkout.PolicyId);
            return View(checkout);
        }

        // GET: /Borrower/List
        // Prints out a list of authors with an action link that directs to
        // another action with the Id of the selected author.
        // Optionally lists only items specified in ids.

        public ActionResult List(string ListAction,
                                 string ListLabel,
                                 int[] ids,
                                 string ListLabelClass,
                                 bool? Partial)
        {
            if (ListLabel != null)
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
                List<Checkout> checkoutsToList = db.Checkouts.Where(a => ids.Contains(a.Id))
                                                       .ToList();
                if (Partial == true)
                    return PartialView(checkoutsToList);
                else
                    return View(checkoutsToList);
            }
            else
            {
                // List everything
                var allCheckouts = db.Checkouts.ToList();
                if (allCheckouts != null)
                {
                    if (Partial == true)
                        return PartialView(allCheckouts);
                    else
                        return View(allCheckouts);
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
