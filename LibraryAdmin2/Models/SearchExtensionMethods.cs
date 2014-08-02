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
        public static int[] Search(this DbSet<Borrower> borrowerDbSet, SearchViewModel searchParams)
        {
            int[] ids = borrowerDbSet.Where(b => b.FirstName.Contains(searchParams.FirstName)).Union(
            borrowerDbSet.Where(b => b.LastName.Contains(searchParams.LastName)))
                        .Select(a => a.Id)
                        .ToArray();
            return ids;
        }


    }
}