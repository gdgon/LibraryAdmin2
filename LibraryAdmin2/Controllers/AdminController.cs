using LibraryAdmin2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LibraryAdmin2.Controllers
{
    public class AdminController : Controller
    {
        private LibraryAdmin2Db db = new LibraryAdmin2Db();

        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/ListCheckoutRequests
        public ActionResult ListCheckoutRequests ()
        {
            var requests = db.CheckoutRequests.Where(r => r.Status == CheckoutRequest.RequestStatus.Pending)
                                              .OrderBy(r => r.Date)
                                              .ToList();
            return View(requests);
        }

        //
        // Get /Admin/ReviewCheckoutRequest
        public ActionResult ReviewCheckoutRequest (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            CheckoutRequest request = db.CheckoutRequests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
         }

	}
}