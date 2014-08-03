namespace LibraryAdmin2.Migrations.ApplicationDbContext
{
    using LibraryAdmin2.Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ApplicationDbContext : DbMigrationsConfiguration<LibraryAdmin2.Models.ApplicationDbContext>
    {
        public ApplicationDbContext()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LibraryAdmin2.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var hasher = new PasswordHasher();
            context.Users.AddOrUpdate(
                u => u.UserName,
                new ApplicationUser { UserName = "admin",
                                      PasswordHash = hasher.HashPassword("123456") });
        }
    }
}
