using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryAdmin2.Models
{
    public class CheckoutRequest
    {
        public int Id { get; set; }
        [Display(Name="Borrower's First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Borrower's Last Name")]
        public string LastName { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public DateTime Date { get; set; }
        public RequestStatus Status { get; set; }

        public enum RequestStatus
        {
            Pending,
            Rejected,
            Approved
        }

        public enum CreateRequestResult
        {
            Success,
            Failed,
            NoCopiesAvailable
        }

        public static CreateRequestResult Request(Book book, string FirstName, string LastName, LibraryAdmin2Db db)
        {
            if (book.AvailableCopies > 0)
            {
                var request = new CheckoutRequest();
                request.Book = book;
                request.FirstName = FirstName;
                request.LastName = LastName;
                request.Status = RequestStatus.Pending;
                request.Date = DateTime.Now;
                request.Book.AvailableCopies -= 1;
                db.CheckoutRequests.Add(request);
                db.SaveChanges();

                return CreateRequestResult.Success;
            }
            else
                return CreateRequestResult.NoCopiesAvailable;
        }

        public static void RejectRequest(CheckoutRequest request, LibraryAdmin2Db db)
        {
            request.Status = RequestStatus.Rejected;
            request.Book.AvailableCopies += 1;
            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void ApproveRequest(CheckoutRequest request, Borrower borrower, Policy policy, LibraryAdmin2Db db)
        {
            var checkout = new Checkout(request, borrower, policy, db);
        }
    }
}