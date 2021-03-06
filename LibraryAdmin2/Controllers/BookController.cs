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
    public class BookController : Controller
    {
        private LibraryAdmin2Db db = new LibraryAdmin2Db();

        // GET: /Book/
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: /Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: /Book/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name");
            return View();
        }

        // POST: /Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Isbn,ImageUrl,ShortDescription,Description,AvailableCopies,AuthorId")] Book book, int[] AuthorId)
        {
            if (ModelState.IsValid)
            {
                //book.BookAuthors = new List<BookAuthor>();
                //for (var i = 0; i < authorId.Length; i++)
                //{

                //    Author a = db.Authors.Find(authorId[i]);
                //    book.BookAuthors.Add(new BookAuthor
                //    {
                //        Author = a,
                //        Book = book
                //    });
                //    var a1 = book.BookAuthors.Last();
                //    //a1.Author = null;
                //}

                book.Authors = new List<Author>();
                for (var i = 0; i < AuthorId.Length; i++)
                {
                    Author a = db.Authors.Find(AuthorId[i]);
                    book.Authors.Add(a);
                }

                // Remove duplicates (untested)
                book.Authors = book.Authors.GroupBy(a => a.Id).Select(x => x.First()).ToList();

                if (Book.Create(book, db))
                    return RedirectToAction("List");
                else
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return View(book);
        }

        // GET: /Book/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Isbn,ImageUrl,ShortDescription,Description,AvailableCopies")] Book book)
        {
            if (ModelState.IsValid)
            {
                if (Book.Edit(book, db))
                    return RedirectToAction("Index");
                else
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return View(book);
        }

        // GET: /Book/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /Book/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            if (Book.Delete(book, db))
                return RedirectToAction("Index");
            else
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }

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
                List<Book> booksToList = db.Books.Where(b => ids.Contains(b.Id))
                                                 .ToList();
                if (Partial == true)
                    return PartialView(booksToList);
                else
                    return View(booksToList);
            }
            else
            {
                // List everything
                if (Partial == true)
                    return PartialView(db.Books.ToList());
                else
                    return View(db.Books.ToList());
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
