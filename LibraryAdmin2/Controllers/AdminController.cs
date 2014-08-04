using LibraryAdmin2.Models;
using LibraryAdmin2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LibraryAdmin2.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private LibraryAdmin2Db db = new LibraryAdmin2Db();

        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/ListCheckoutRequests
        public ActionResult ListCheckoutRequests()
        {
            var requests = db.CheckoutRequests.Where(r => r.Status == CheckoutRequest.RequestStatus.Pending)
                                              .OrderBy(r => r.Date)
                                              .ToList();
            return View(requests);
        }

        //
        // GET: /Admin/ReviewCheckoutRequest
        public ActionResult ReviewCheckoutRequest(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var request = db.CheckoutRequests.Find(id);

            if (request == null)
            {
                return HttpNotFound();
            }

            // Borrower
            var matches = db.Borrowers.Where(b => b.FirstName.Contains(request.FirstName))
                                  .Intersect(
                      db.Borrowers.Where(b => b.LastName.Contains(request.LastName))).ToList();

            if (matches.Count == 1)
                ViewBag.Borrower = matches.First();

            // Policy
            ViewBag.PolicyId = new SelectList(db.Policies, "Id", "Name");

            return View(request);
        }

        public ActionResult Approve(int RequestId, int PolicyId, int BorrowerId)
        {
            var request = db.CheckoutRequests.Find(RequestId);
            var policy = db.Policies.Find(PolicyId);
            var borrower = db.Borrowers.Find(BorrowerId);
            var book = request.Book;
            request.Approve(book, borrower, policy, db);

            return View();
        }

        public ActionResult Reject(int RequestId)
        {
            var request = db.CheckoutRequests.Find(RequestId);
            request.Reject(db);
            return View();
        }

        public ActionResult Return()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Return(int CheckoutId)
        {
            var checkout = db.Checkouts.Find(CheckoutId);
            checkout.Return(db);
            return View("Returned");
        }
    }
}