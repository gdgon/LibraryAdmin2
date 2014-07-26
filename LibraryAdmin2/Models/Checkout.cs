using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAdmin2.Models
{
    public class Checkout
    {
        public int Id { get; set; }
        public virtual int BookId { get; set; }
        public virtual Book Book { get; set; }
        public virtual int PolicyId { get; set; }
        public virtual Policy Policy { get; set; }
        public virtual DateTime CheckoutDate { get; set; }
        public virtual DateTime DueDate { get; set; }
        public CheckoutStatus Status { get; set; }

        public enum CheckoutStatus
        {
            Out,
            Returned,
            Void            
        }
    }
}