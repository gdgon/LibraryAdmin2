using LibraryAdmin2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryAdmin2.Models
{
    public static class SearchExtensionMethods
    {
        // Borrower search
        public static int[] Search(this DbSet<Borrower> borrowerDbSet, SearchViewModel searchParams)
        {
            int[] ids = borrowerDbSet.Where(b => b.FirstName.Contains(searchParams.FirstName)).Union(
            borrowerDbSet.Where(b => b.LastName.Contains(searchParams.LastName)))
                        .Select(a => a.Id)
                        .ToArray();
            return ids;
        }

        // Author search
        public static int[] Search(this DbSet<Author> authorDbSet, SearchViewModel searchParams)
        {
            int[] ids = authorDbSet.Where(a => a.FirstName.Contains(searchParams.FirstName)).Union(
            authorDbSet.Where(a => a.LastName.Contains(searchParams.LastName)))
                        .Select(a => a.Id)
                        .ToArray();
            return ids;
        }

        // Book search
        public static int[] Search(this DbSet<Book> bookDbSet, SearchViewModel searchParams)
        {
            int[] ids = bookDbSet.Where(a => a.Title.Contains(searchParams.Title)).Union(
                        bookDbSet.Where(a => a.Isbn.Contains(searchParams.Isbn)))
                                .Select(a => a.Id)
                                .ToArray();
            return ids;
        }

        // Checkout search
        public static int[] Search(this DbSet<Checkout> checkoutDbSet, SearchViewModel searchParams)
        {
            var tmp = checkoutDbSet.Where(b => b.Borrower.FirstName.Contains(searchParams.FirstName)).Union(
                        checkoutDbSet.Where(b => b.Borrower.LastName.Contains(searchParams.LastName)));
            var ids = checkoutDbSet.Where(b => b.Status == LibraryAdmin2.Models.Checkout.CheckoutStatus.Out)
                                  .Intersect(tmp)
                                  .Select(a => a.Id)
                                  .ToArray();
            return ids;
        }
    }
}