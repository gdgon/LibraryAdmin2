using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAdmin2.Models
{
    public class LogEvent
    {
        public LogEvent()
        {

        }

        public LogEvent(string message, EventTypes eventType, LibraryAdmin2Db db)
        {
            Message = message;
            Event = eventType;
            Date = DateTime.Now;
            db.LogEvents.Add(this);
            db.SaveChanges();
        }

        public int Id { get; set; }
        public string Message { get; set; }
        public EventTypes Event { get; set; }
        public DateTime Date { get; private set; }

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
    }
}