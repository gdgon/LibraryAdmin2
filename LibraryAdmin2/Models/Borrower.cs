using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        //[Column(TypeName = "DateTime2")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name ="Guardian's Phone Number")]
        public string GuardianPhone { get; set; }

        public static bool Create(Borrower borrower, LibraryAdmin2Db db)
        {
            borrower.Name = borrower.FirstName + " " + borrower.LastName;
            db.Borrowers.Add(borrower);
            db.SaveChanges();
            new LogEvent("(BorrowerId:" + borrower.Id + ") \"" + borrower.Name + "\" created.", LogEvent.EventTypes.BorrowerCreate, db);
            return true;
        }

        public static bool Edit(Borrower borrower, LibraryAdmin2Db db)
        {
            borrower.Name = borrower.FirstName + " " + borrower.LastName;
            db.Entry(borrower).State = EntityState.Modified;
            db.SaveChanges();
            new LogEvent("(BorrowerId:" + borrower.Id + ") \"" + borrower.Name + "\" properties edited.", LogEvent.EventTypes.BorrowerEdit, db);
            return true;
        }

        public static bool Delete(Borrower borrower, LibraryAdmin2Db db)
        {
            db.Borrowers.Remove(borrower);
            db.SaveChanges();
            new LogEvent("(BorrowerId:" + borrower.Id + ") \"" + borrower.Name + "\" deleted.", LogEvent.EventTypes.BorrowerDelete, db);
            return true;
        }
    }
}