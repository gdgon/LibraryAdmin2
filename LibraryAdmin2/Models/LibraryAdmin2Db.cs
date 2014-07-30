using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryAdmin2.Models
{
    public class LibraryAdmin2Db : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<CheckoutRequest> CheckoutRequests { get; set; }
        public DbSet<LogEvent> LogEvents { get; set; }
    }
}