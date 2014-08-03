namespace LibraryAdmin2.Migrations.LibraryAdminDb
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class LibraryAdmin2Db : DbMigrationsConfiguration<LibraryAdmin2.Models.LibraryAdmin2Db>
    {
        public LibraryAdmin2Db()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LibraryAdmin2.Models.LibraryAdmin2Db context)
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
        }
    }
}
