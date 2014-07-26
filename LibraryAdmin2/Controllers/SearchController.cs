using LibraryAdmin2.Models;
using LibraryAdmin2.Utils;
using LibraryAdmin2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LibraryAdmin2.Controllers
{
    public class SearchController : Controller
    {
        private LibraryAdmin2Db db = new LibraryAdmin2Db();

        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Search/Author
        public ActionResult Author(bool? partial)
        {
        return View();
        }

        // POST: /Search/Author
        [HttpPost]
        public ActionResult Author(NameViewModel name)
        {
            // Get search matches
            int[] ids = db.Authors.Where(a => a.FirstName.Contains(name.FirstName)).Intersect(
                        db.Authors.Where(a => a.LastName.Contains(name.LastName)))
                                    .Select(a => a.Id)
                                    .ToArray();

            // Show results
            if (ids.Count() == 0)
            {
                // No match
                return View("NoResultsFound");
            }
            else
            {
                // Match found
                // Redirect to target List action with relevant URL params
                UrlBuilder url = new UrlBuilder(this, "List", "Author");
                //url.AppendParam("toAction", "#");
                //url.AppendParam("actionLabel", "Select");
                url.AppendParam("partial", true);
                //url.AppendParam("actionLabelClass", "btn-select");
                for (var i = 0; i < ids.Length; i++)
                {
                    url.AppendParam("ids", ids[i]);
                }
                return Redirect(url.ToString());
            }
        }

        // GET: /Search/Book
        public ActionResult Book(bool? partial)
        {
            return View();
        }

        // POST: /Search/Book
        [HttpPost]
        public ActionResult Book(string Title, string Isbn)
        {
            // Get search matches
            int[] ids = db.Books.Where(a => a.Title.Contains(Title)).Intersect(
                        db.Books.Where(a => a.Isbn.Contains(Isbn)))
                                .Select(a => a.Id)
                                .ToArray();

            // Show results
            if (ids.Count() == 0)
            {
                // No match
                return PartialView("NoResultsFound");
            }
            else
            {
                // Match found
                // Redirect to target List action with relevant URL params
                UrlBuilder url = new UrlBuilder(this, "List", "Book");
                //url.AppendParam("toAction", "#");
                //url.AppendParam("actionLabel", "Select");
                url.AppendParam("partial", true);
                //url.AppendParam("actionLabelClass", "btn-select");
                for (var i = 0; i < ids.Length; i++)
                    url.AppendParam("ids", ids[i]);

                return Redirect(url.ToString());
            }
        }

        // GET: /Search/Borrower
        public ActionResult Borrower(bool? partial)
        {
            return View();
        }

        // POST: /Search/Borrower
        [HttpPost]
        public ActionResult Borrower(string FirstName, string LastName)
        {
            // Get search matches
            int[] ids = db.Borrowers.Where(b => b.FirstName.Contains(FirstName)).Intersect(
                        db.Borrowers.Where(b => b.LastName.Contains(LastName)))
                                    .Select(a => a.Id)
                                    .ToArray();

            // Show results
            if (ids.Count() == 0)
            {
                // No match
                return View("NoResultsFound");
            }
            else
            {
                // Match found
                // Redirect to target List action with relevant URL params
                UrlBuilder url = new UrlBuilder(this, "List", "Author");
                //url.AppendParam("toAction", "#");
                //url.AppendParam("actionLabel", "Select");
                url.AppendParam("partial", true);
                //url.AppendParam("actionLabelClass", "btn-select");
                for (var i = 0; i < ids.Length; i++)
                {
                    url.AppendParam("ids", ids[i]);
                }
                return Redirect(url.ToString());
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