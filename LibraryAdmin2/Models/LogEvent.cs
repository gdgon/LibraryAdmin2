using System;
using System.Collections.Generic;
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
        public int BorrowerId { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public int CheckoutId { get; set; }
        public int RequestId { get; set; }
        public int PolicyId { get; set; }

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
            Write(new LogEvent { Event = EventTypes.BookCreate, BookId = BookIdParam }, db);
        }

        public static void BookEdit(int BookIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.BookEdit, BookId = BookIdParam }, db);
        }

        public static void BookDelete(int BookIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.BookDelete, BookId = BookIdParam }, db);
        }

        public static void AuthorCreate(int AuthorIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.AuthorCreate, AuthorId = AuthorIdParam }, db);
        }

        public static void AuthorEdit(int AuthorIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.AuthorEdit, AuthorId = AuthorIdParam }, db);
        }

        public static void AuthorDelete(int AuthorIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.AuthorDelete, AuthorId = AuthorIdParam }, db);
        }

        public static void BorrowerCreate(int BorrowerIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.BorrowerCreate, BorrowerId = BorrowerIdParam }, db);
        }

        public static void BorrowerEdit(int BorrowerIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.BorrowerEdit, BorrowerId = BorrowerIdParam }, db);
        }

        public static void BorrowerDelete(int BorrowerIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.BorrowerDelete, BorrowerId = BorrowerIdParam }, db);
        }

        public static void RequestNew(int RequestIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.RequestNew, RequestId = RequestIdParam }, db);
        }

        public static void RequestApproved(int RequestIdParam, int BorrowerIdParam, int PolicyIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.RequestApproved, RequestId = RequestIdParam, BorrowerId = BorrowerIdParam, PolicyId = PolicyIdParam }, db);
        }

        public static void RequestRejected(int RequestIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.RequestRejected, RequestId = RequestIdParam }, db);
        }

        public static void CheckoutNew(int CheckoutIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.CheckoutNew, CheckoutId = CheckoutIdParam }, db);
        }

        public static void CheckoutReturn(int CheckoutIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.CheckoutReturn, CheckoutId = CheckoutIdParam }, db);
        }

        public static void CheckoutVoid(int CheckoutIdParam, LibraryAdmin2Db db)
        {
            Write(new LogEvent { Event = EventTypes.CheckoutVoid, CheckoutId = CheckoutIdParam }, db);
        }

    }
}