using LibraryAdmin2.Models;
using LibraryAdmin2.Utils;
using LibraryAdmin2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        // GET: /Search/Search
        public ActionResult Search(bool? Book, bool? Author, bool? Checkout)
        {
            var list = new List<SelectListItem>();

            if (Book == true)
                list.Add(new SelectListItem { Value = "Book" });
            if (Book == true)
                list.Add(new SelectListItem { Value = "Author" });
            if (Book == true)
                list.Add(new SelectListItem { Value = "Checkout" });
            if (Book == null && Author == null && Checkout == null)
            {
                // If no parameters are given
                list.Add(new SelectListItem { Value = "Book" });
                list.Add(new SelectListItem { Value = "Author" });
            }

            ViewBag.SearchType = new SelectList(list, "Value", "Value");
            return View();
        }

        // POST: /Search/Search
        [HttpPost]
        public ActionResult Search(SearchViewModel searchParams)
        {
            int[] ids;
            switch (searchParams.SearchType)
            {
                case "Borrower":
                    ids = db.Borrowers.Search(searchParams);
                    break;
                case "Author":
                    ids = db.Authors.Search(searchParams);
                    break;
                case "Book":
                    ids = db.Books.Search(searchParams);
                    break;
                case "Checkout":
                    ids = db.Checkouts.Search(searchParams);
                    break;
                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ids.Length == 0)
                return View("NoResultsFound");
            else if (ids.Length > 0)
                return DispatchToList(ids, searchParams.SearchType, searchParams.Partial);
            else
                return View(searchParams);
        }

        private ActionResult DispatchToList(int[] ids, string searchType, bool? partial)
        {
            if (ids == null || searchType == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
            {
                UrlBuilder url = new UrlBuilder(this, "List", searchType);
                //url.AppendParam("actionLabelClass", "btn-select");
                if (partial == true)
                    url.AppendParam("partial", true);
                //if (toAction != null)
                //    url.AppendParam("toAction", toAction);
                //if (actionLabel != null)
                //    url.AppendParam("actionLabel", actionLabel);
                //if (actionLabelClass != null)
                //    url.AppendParam("actionLabelClass", actionLabelClass);
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