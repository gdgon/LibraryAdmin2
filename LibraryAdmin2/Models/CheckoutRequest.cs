using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAdmin2.Models
{
    public class CheckoutRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime Date { get; set; }
        public RequestStatus Status { get; set; }

        public enum RequestStatus
        {
            Pending,
            Rejected,
            Approved
        }

        public enum CreateRequestResults
        {
            Success,
            Failed,
            NoCopiesAvailable
        }

        public static CreateRequestResults Request(Book book, string FirstName, string LastName, LibraryAdmin2Db db)
        {
            if (book.AvailableCopies > 0)
            {
                var request = new CheckoutRequest();
                request.Book = book;
                request.FirstName = FirstName;
                request.LastName = LastName;
                db.CheckoutRequests.Add(request);
                db.SaveChanges();

                return CreateRequestResults.Success;
            }
            else
                return CreateRequestResults.NoCopiesAvailable;
        }
    }
}