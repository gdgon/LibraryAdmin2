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
    // Search(string Action, string[] Fields, bool partial?)
    //  -> if partial, ViewBag.partial = true; PartialView()
    //  -> on View: show form with fields
    //      -> on submit
    //          -> flatten Fields[], partial and submit to Action
    // Action(bool? partial, string Field1, string Field2 ...)
    //  -> if partial, PartialView
    //  -> get result Ids
    //  -> build URL string: model List() action, append partial, append Ids
    //  -> redirect to URL

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
        public ActionResult Author(SearchViewModel searchParams)
        {
            int[] ids = db.Authors.Search(searchParams);

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
            int[] ids = db.Books.Search(searchParams);

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
            ViewBag.partial = true;
            return View();
        }

        // POST: /Search/Borrower
        [HttpPost]
        public ActionResult Borrower(string FirstName,
                                     string LastName,
                                     bool? partial,
                                     string toAction,
                                     string actionLabel,
                                     string actionLabelClass)
        {
            int[] ids = db.Borrowers.Search(searchParams);

            // Show results
            if (ids.Count() == 0)
                // No match
                if (partial == true)
                    return PartialView("NoResultsFound");
                else
                    return View("NoResultsFound");
            else
            {
                // Match found
                // Redirect to target List action with relevant URL params
                UrlBuilder url = new UrlBuilder(this, "List", "Borrower");
                if (partial == true)
                    url.AppendParam("partial", true);
                if (toAction != null)
                    url.AppendParam("toAction", toAction);
                if (actionLabel != null)
                    url.AppendParam("actionLabel", actionLabel);
                if (actionLabelClass != null)
                    url.AppendParam("actionLabelClass", actionLabelClass);
                for (var i = 0; i < ids.Length; i++)
                    url.AppendParam("ids", ids[i]);
                return Redirect(url.ToString());
            }
        }


        // POST: /Search/Checkout
        [HttpPost]
        public ActionResult Checkout(string FirstName,
                                     string LastName,
                                     bool? partial,
                                     string toAction,
                                     string actionLabel,
                                     string actionLabelClass)
        {
            int[] ids = db.Checkouts.Search(searchParams);

            // Show results
            if (ids.Count() == 0)
                // No match
                if (partial == true)
                    return PartialView("NoResultsFound");
                else
                    return View("NoResultsFound");
            else
            {
                // Match found
                // Redirect to target List action with relevant URL params
                UrlBuilder url = new UrlBuilder(this, "List", "Checkout");
                if (partial == true)
                    url.AppendParam("partial", true);
                if (toAction != null)
                    url.AppendParam("toAction", toAction);
                if (actionLabel != null)
                    url.AppendParam("actionLabel", actionLabel);
                if (actionLabelClass != null)
                    url.AppendParam("actionLabelClass", actionLabelClass);
                for (var i = 0; i < ids.Length; i++)
                    url.AppendParam("ids", ids[i]);
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