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

        // GET: /Search/PopupAuthor
        public ActionResult PopupAuthor(bool? partial)
        {
        return PartialView();
        }

        // POST: /Search/PopupAuthor
        [HttpPost]
        public ActionResult PopupAuthor(NameViewModel name)
        {
            // Get search matches
            int[] ids = db.Authors.Where(a => a.FirstName.Contains(name.FirstName)).Union(
                        db.Authors.Where(a => a.LastName.Contains(name.LastName)))
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
                UrlBuilder url = new UrlBuilder(this, "List", "Author");
                url.AppendParam("toAction", "#");
                url.AppendParam("actionLabel", "Select");
                url.AppendParam("partial", true);
                url.AppendParam("actionLabelClass", "btn-select");
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