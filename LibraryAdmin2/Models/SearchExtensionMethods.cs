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
        public static int[] Search(this DbSet<Borrower> borrowerParam, SearchViewModel searchParams, LibraryAdmin2Db db)
        {
            int[] ids = db.Borrowers.Where(b => b.FirstName.Contains(searchParams.FirstName)).Union(
            db.Borrowers.Where(b => b.LastName.Contains(searchParams.LastName)))
                        .Select(a => a.Id)
                        .ToArray();
            return ids;
        }


    }
}