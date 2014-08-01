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
    public class BorrowerController : Controller
    {
        private LibraryAdmin2Db db = new LibraryAdmin2Db();

        // GET: /Borrower/
        public ActionResult Index()
        {
            return View(db.Borrowers.ToList());
        }

        // GET: /Borrower/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = db.Borrowers.Find(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // GET: /Borrower/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Borrower/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,FirstName,LastName,DateOfBirth")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                Borrower.Create(borrower, db);
                return RedirectToAction("Index");
            }

            return View(borrower);
        }

        // GET: /Borrower/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = db.Borrowers.Find(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // POST: /Borrower/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,FirstName,LastName")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                Borrower.Edit(borrower, db);
                return RedirectToAction("Index");
            }
            return View(borrower);
        }

        // GET: /Borrower/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = db.Borrowers.Find(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // POST: /Borrower/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrower borrower = db.Borrowers.Find(id);
            Borrower.Delete(borrower, db);
            return RedirectToAction("Index");
        }

        // GET: /Borrower/List
        // Prints out a list of authors with an action link that directs to
        // another action with the Id of the selected author.
        // Optionally lists only items specified in ids.

        public ActionResult List(string toAction,
                                 string actionLabel,
                                 int[] ids,
                                 string actionLabelClass,
                                 bool? partial)
        {
            if (toAction != null)
            {
                ViewBag.ToAction = toAction;

                if (actionLabel != null)
                    ViewBag.ActionLabel = actionLabel;
                else
                    ViewBag.ActionLabel = "Select";

                if (actionLabelClass != null)
                    ViewBag.ActionLabelClass = actionLabelClass;

            }

            if (ids != null)
            {
                // List specified subset 
                List<Borrower> borrowersToList = db.Borrowers.Where(a => ids.Contains(a.Id))
                                                       .ToList();
                if (partial == true)
                    return PartialView(borrowersToList);
                else
                    return View(borrowersToList);
            }
            else
            {
                // List everything
                var allBorrowers = db.Borrowers.ToList();
                if (allBorrowers != null)
                {
                    if (partial == true)
                        return PartialView(allBorrowers);
                    else
                        return View(allBorrowers);
                }
                else
                    return View();
            }
        }

        public ActionResult TopBorrower()
        {
            var topBorrower = db.LogEvents.Where(l => l.Event == LogEvent.EventTypes.CheckoutNew).Count();
            return View();
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
