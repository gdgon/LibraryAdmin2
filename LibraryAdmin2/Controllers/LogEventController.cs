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
    public class LogEventController : Controller
    {
        private LibraryAdmin2Db db = new LibraryAdmin2Db();

        // GET: LogEvents
        public ActionResult Index()
        {
            return View(db.LogEvents.ToList());
        }

        // GET: LogEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogEvent logEvent = db.LogEvents.Find(id);
            if (logEvent == null)
            {
                return HttpNotFound();
            }
            return View(logEvent);
        }

        // GET: /Borrower/List
        // Prints out a list of items with an action link that directs to
        // another action with the Id of the selected items.
        // Optionally lists only items specified in ids.

        public ActionResult List(string ListAction,
                                 string ListLabel,
                                 int[] ids,
                                 string ListLabelClass,
                                 bool partial = false)
        {
            ViewBag.Db = db;
            if (ListAction != null)
            {
                ViewBag.ListAction = ListAction;

                if (ListLabel != null)
                    ViewBag.ListLabel = ListLabel;
                else
                    ViewBag.ListLabel = "Select";

                if (ListLabelClass != null)
                    ViewBag.ListLabelClass = ListLabelClass;

            }

            if (ids != null)
            {
                // List specified subset 
                List<LogEvent> events = db.LogEvents.Where(e => ids.Contains(e.Id))
                                                    .ToList();
                if (partial == true)
                    return PartialView(events);
                else
                    return View(events);
            }
            else
            {
                // List everything
                var events = db.LogEvents.ToList();
                if (events != null)
                {
                    if (partial == true)
                        return PartialView(events);
                    else
                        return View(events);
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
