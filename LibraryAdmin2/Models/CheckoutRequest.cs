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
                new LogEvent("Checkout request (RequestId:"
  + request.Id + ") made by \"" + FirstName + " " + LastName + "for (BookId:" + book.Id + ") \"" + book.Title + "\".", LogEvent.EventTypes.RequestNew, db);

                return CreateRequestResult.Success;
            }
            else
                return CreateRequestResult.NoCopiesAvailable;
        }

        public void Reject(LibraryAdmin2Db db)
        {
            Status = RequestStatus.Rejected;
            Book.AvailableCopies += 1;
            db.Entry(this).State = EntityState.Modified;
            db.SaveChanges();
            new LogEvent("REJECTED checkout request (RequestId:"
      + Id + "for (BookId:" + Book.Id + ") \"" + Book.Title + "\".", LogEvent.EventTypes.RequestRejected, db);
        }

        public void Approve(Borrower borrower, Policy policy, LibraryAdmin2Db db)
        {
            var checkout = new Checkout(this, borrower, policy, db);
            db.Entry(this).State = EntityState.Modified;
            Status = RequestStatus.Approved;
            db.SaveChanges();
            new LogEvent("APPROVED checkout request (RequestId:"
     + Id + "for (BookId:" + Book.Id + ") \"" + Book.Title + "\" with (CheckoutId:" + checkout.Id + ").", LogEvent.EventTypes.RequestApproved, db);
        }
    }
}