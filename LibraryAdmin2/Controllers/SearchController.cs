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
        public ActionResult Search(SearchViewModel searchParams)
        {
            var searchTypeList = new List<SelectListItem>();

            if (searchParams.SearchType != null)
            {
                searchTypeList.Add(new SelectListItem { Value = searchParams.SearchType });
            }
            else
            {
                // Default values for borrower frontend
                searchTypeList.Add(new SelectListItem { Value = "Book" });
                searchTypeList.Add(new SelectListItem { Value = "Author" });
            }
            ViewBag.SearchType = new SelectList(searchTypeList, "Value", "Value");

            if (searchParams.Partial == true)
                return PartialView(searchParams);
            else
                return View(searchParams);
        }

        // POST: /Search/Search
        [HttpPost]
        public ActionResult Search(SearchViewModel searchParams, bool? dudd)
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
            else
                return DispatchToList(ids, searchParams);
        }

        private ActionResult DispatchToList(int[] ids, SearchViewModel searchParams)
        {
            if (ids == null || searchParams.SearchType == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
            {
                UrlBuilder url = new UrlBuilder(this, "List", searchParams.SearchType);
                //url.AppendParam("ListLabelClass", "btn-select");
                
                    url.AppendParam("Partial", true);
                if (searchParams.ListAction != null)
                    url.AppendParam("ListAction", searchParams.ListAction);
                if (searchParams.ListLabel != null)
                    url.AppendParam("ListLabel", searchParams.ListLabel);
                if (searchParams.ListLabelClass != null)
                    url.AppendParam("ListLabelClass", searchParams.ListLabelClass);
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