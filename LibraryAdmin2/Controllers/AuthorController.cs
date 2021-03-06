﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryAdmin2.Models;
using LibraryAdmin2.ViewModels;

namespace LibraryAdmin2.Controllers
{
    public class AuthorController : Controller
    {
        private LibraryAdmin2Db db = new LibraryAdmin2Db();

        // GET: /Author/
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: /Author/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            var bookArray = db.Books.Include(b => b.Authors)
                                     .Where(b => b.Authors.Any(a => a.Id == id))
                                     .Select(b => b.Id)
                                     .ToArray();
            ViewBag.BookIds = bookArray;
            return View(author);
        }

        // GET: /Author/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Author/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,ImageUrl,ShortDescription,Description")] Author author)
        {
            if (ModelState.IsValid)
            {
                if (Author.Create(author, db))
                    return RedirectToAction("List");
                else
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return View(author);
        }

        // GET: /Author/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: /Author/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,ImageUrl,ShortDescription,Description")] Author author)
        {
            if (ModelState.IsValid)
            {
                if (Author.Edit(author, db))
                    return RedirectToAction("Index");
                else
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return View(author);
        }

        // GET: /Author/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: /Author/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.Authors.Find(id);
            if (Author.Delete(author, db))
                return RedirectToAction("Index");
            else
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }

        // GET: /Author/List
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
                List<Author> authorsToList = db.Authors.Where(a => ids.Contains(a.Id))
                                                       .ToList();
                if (Partial == true)
                    return PartialView(authorsToList);
                else
                    return View(authorsToList);
            }
            else
            {
                // List everything
                var allAuthors = db.Authors.ToList();
                if (allAuthors != null)
                {
                    if (Partial == true)
                        return PartialView(allAuthors);
                    else
                        return View(allAuthors);
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
