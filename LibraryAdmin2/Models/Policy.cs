using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAdmin2.Models
{
    public class Policy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DaysAllowed { get; set; }
        public int Penalty { get; set; }

        public static bool Create(Policy policy, LibraryAdmin2Db db)
        {
            db.Policies.Add(policy);
            db.SaveChanges();
            return true;
        }

        public static bool Edit(Policy policy, LibraryAdmin2Db db)
        {
            db.Entry(policy).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public static bool Delete(Policy policy, LibraryAdmin2Db db)
        {
            db.Policies.Remove(policy);
            db.SaveChanges();
            return true;
        }
    }
}