using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryAdmin2.Models
{
    public class Checkout
    {
        public Checkout()
        {
        }

        public Checkout(CheckoutRequest request, Borrower borrower, Policy policy, LibraryAdmin2Db db)
        {
            var checkout = new Checkout();
            checkout.Book = request.Book;
            checkout.Policy = policy;
            checkout.CheckoutDate = DateTime.Now;
            checkout.Status = CheckoutStatus.Out;
            checkout.DueDate = CalculateDueDate(policy);
            db.Entry(checkout).State = EntityState.Modified;
            db.SaveChanges();
        }

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

        public static void Return(Checkout checkout, LibraryAdmin2Db db)
        {
            checkout.Book.AvailableCopies += 1;
            checkout.Status = CheckoutStatus.Returned;
            db.Entry(checkout).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static DateTime CalculateDueDate(Policy policy)
        {
            // TODO: Don't count weekends, maybe holidays.
            var date = DateTime.Now.Date.AddDays(policy.DaysAllowed);
            return (date);
        }
    }
}