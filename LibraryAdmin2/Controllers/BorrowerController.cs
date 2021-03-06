﻿using System;
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
            return RedirectToAction("List");
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
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Borrower/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrower borrower = db.Borrowers.Find(id);
            Borrower.Delete(borrower, db);
            return RedirectToAction("Index");
        }

        // GET: /Boorower/List
        // Prints out a list of authors with an action link that directs to
        // another action with the Id of the selected author.
        // Optionally lists only items specified in ids.

        public ActionResult List(string ListAction,
                                 string ListActionController,
                                 string ListLabel,
                                 int[] ids,
                                 string ListLabelClass,
                                 bool? Partial)
        {
            if (ListLabel != null)
            {
                ViewBag.ListLabel = ListLabel;

                if (ListAction != null)
                ViewBag.ListAction = ListAction;

                if (ListActionController != null)
                    ViewBag.ListActionController = ListActionController;

                if (ListLabelClass != null)
                    ViewBag.ListLabelClass = ListLabelClass;

            }

            if (ids != null)
            {
                // List specified subset 
                List<Borrower> borrowersToList = db.Borrowers.Where(a => ids.Contains(a.Id))
                                                       .ToList();
                if (Partial == true)
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
                    if (Partial == true)
                        return PartialView(allBorrowers);
                    else
                        return View(allBorrowers);
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
