using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryAdmin2.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name ="Guardian's Phone Number")]
        public string GuardianPhone { get; set; }

        public static bool Create(Borrower borrower, LibraryAdmin2Db db)
        {
            borrower.Name = borrower.FirstName + " " + borrower.LastName;
            db.Borrowers.Add(borrower);
            db.SaveChanges();
            return true;
        }

        public static bool Edit(Borrower borrower, LibraryAdmin2Db db)
        {
            db.Entry(borrower).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public static bool Delete(Borrower borrower, LibraryAdmin2Db db)
        {
            db.Borrowers.Remove(borrower);
            db.SaveChanges();
            return true;
        }
    }
}