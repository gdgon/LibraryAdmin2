using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LibraryAdmin2.Models
{
    public class LogEvent
    {
        public LogEvent()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; }
        public EventTypes Event { get; set; }
        public DateTime Date { get; private set; }
        [Display(Name="Borrower")]
        public int BorrowerId { get; set; }
        [Display(Name = "Book")]
        public int BookId { get; set; }
        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        [Display(Name = "Checkout")]
        public int CheckoutId { get; set; }
        [Display(Name = "Request")]
        public int RequestId { get; set; }
        [Display(Name = "Policy")]
        public int PolicyId { get; set; }
        public string Message { get; set; }

        public enum EventTypes
        {
            BookCreate,
            BookEdit,
            BookDelete,
            AuthorCreate,
            AuthorEdit,
            AuthorDelete,
            BorrowerCreate,
            BorrowerEdit,
            BorrowerDelete,
            RequestNew,
            RequestApproved,
            RequestRejected,
            CheckoutNew,
            CheckoutReturn,
            CheckoutVoid
        }

        public static void Write(LogEvent log, LibraryAdmin2Db db)
        {
            db.LogEvents.Add(log);
            db.SaveChanges();
        }

        public static void BookCreate(int BookIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent
            {
                Event = EventTypes.BookCreate,
                BookId = BookIdParam,
                Message = String.Format("\"{0}\" added to book catalog.", db.Books.Find(BookIdParam).Title)
            }
                , db);
        }

        public static void BookEdit(int BookIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent
            {
                Event = EventTypes.BookEdit,
                BookId = BookIdParam,
                Message = String.Format("\"{0}\" book properties edited.", db.Books.Find(BookIdParam).Title)
            }, db);
        }

        public static void BookDelete(int BookIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent
            {
                Event = EventTypes.BookDelete,
                BookId = BookIdParam,
                Message = String.Format("\"{0}\" deleted.", db.Books.Find(BookIdParam).Title)
            }, db);
        }

        public static void AuthorCreate(int AuthorIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent
            {
                Event = EventTypes.AuthorCreate,
                AuthorId = AuthorIdParam,
                Message = String.Format("\"{0}\" added.", db.Authors.Find(AuthorIdParam).Name)
            }, db);
        }

        public static void AuthorEdit(int AuthorIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent
            {
                Event = EventTypes.AuthorEdit,
                AuthorId = AuthorIdParam,
                Message = String.Format("\"{0}\" author properties edited.", db.Authors.Find(AuthorIdParam).Name)
            }, db);
        }

        public static void AuthorDelete(int AuthorIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent
            {
                Event = EventTypes.AuthorDelete,
                AuthorId = AuthorIdParam,
                Message = String.Format("\"{0}\" deleted.", db.Authors.Find(AuthorIdParam).Name)
            }, db);
        }

        public static void BorrowerCreate(int BorrowerIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent
            {
                Event = EventTypes.BorrowerCreate,
                BorrowerId = BorrowerIdParam,
                Message = String.Format("\"{0}\" borrower added to records.", db.Borrowers.Find(BorrowerIdParam).Name)
            }, db);
        }

        public static void BorrowerEdit(int BorrowerIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent
            {
                Event = EventTypes.BorrowerEdit,
                BorrowerId = BorrowerIdParam,
                Message = String.Format("\"{0}\" borrower information edited.", db.Borrowers.Find(BorrowerIdParam).Name)
            }, db);
        }

        public static void BorrowerDelete(int BorrowerIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent
            {
                Event = EventTypes.BorrowerDelete,
                BorrowerId = BorrowerIdParam,
                Message = String.Format("\"{0}\" borrower deleted.", db.Borrowers.Find(BorrowerIdParam).Name)
            }, db);
        }

        public static void RequestNew(int RequestIdParam, LibraryAdmin2Db db)
        {
            var request = db.CheckoutRequests.Find(RequestIdParam);
            var book = db.Books.Find(request.BookId);
            Write(new LogEvent
            {
                Event = EventTypes.RequestNew,
                RequestId = RequestIdParam,
                Message = String.Format("Request made for book \"{0}\" by \"{1} {2}\".", book.Title, request.FirstName, request.LastName)
            }, db);
        }

        public static void RequestApproved(int RequestIdParam, int BookIdParam, int BorrowerIdParam, int PolicyIdParam, LibraryAdmin2Db db)
        {
            var book = db.Books.Find(BookIdParam);
            var borrower = db.Borrowers.Find(BorrowerIdParam);
            Write(new LogEvent
            {
                Event = EventTypes.RequestApproved,
                RequestId = RequestIdParam,
                BookId = BookIdParam,
                BorrowerId = BorrowerIdParam,
                PolicyId = PolicyIdParam,
                Message = String.Format("APPROVED request for book \"{0}\" by \"{1}\".", book.Title, borrower.Name)
            }, db);
        }

        public static void RequestRejected(int RequestIdParam, LibraryAdmin2Db db)
        {
            var request = db.CheckoutRequests.Find(RequestIdParam);
            var book = db.Books.Find(request.BookId);
            Write(new LogEvent
            {
                Event = EventTypes.RequestRejected,
                RequestId = RequestIdParam,
                Message = String.Format("REJECTED request for book \"{0}\" by \"{1} {2}\".", book.Title, request.FirstName, request.LastName)
            }, db);
        }

        public static void CheckoutNew(int CheckoutIdParam, LibraryAdmin2Db db)
        {
            var checkout = db.Checkouts.Find(CheckoutIdParam);
            Write(new LogEvent
            {
                Event = EventTypes.CheckoutNew,
                CheckoutId = CheckoutIdParam,
                Message = String.Format("New Checkout made for book \"{0}\" by \"{1}\" with policy \"{2}\", due on \"{3}\".", checkout.Book.Title, checkout.Borrower.Name, checkout.Policy.Name, checkout.DueDate.ToString())
            }, db);
        }

        public static void CheckoutReturn(int CheckoutIdParam, LibraryAdmin2Db db)
        {
            var checkout = db.Checkouts.Find(CheckoutIdParam);
            Write(new LogEvent
            {
                Event = EventTypes.CheckoutReturn,
                CheckoutId = CheckoutIdParam,
                BookId = checkout.BookId,
                BorrowerId = checkout.BorrowerId,
                Message = String.Format("Returned book \"{0}\" by borrower \"{1}\". Returned on {2}, due date {3}", checkout.Book.Title, checkout.Borrower.Name, DateTime.Now.Date.Date.ToString(), checkout.DueDate.Date.ToString())
            }, db);
        }

        public static void CheckoutVoid(int CheckoutIdParam, LibraryAdmin2Db db)
        {
            var checkout = db.Checkouts.Find(CheckoutIdParam);
            Write(new LogEvent
            {
                Event = EventTypes.CheckoutVoid,
                CheckoutId = CheckoutIdParam,
                Message = String.Format("Checkout voided for book \"{0}\" by borrower \"{1}\", due on {3}", checkout.Book.Title, checkout.Borrower.Name, checkout.DueDate.Date.ToString())
            }, db);
        }

    }
}