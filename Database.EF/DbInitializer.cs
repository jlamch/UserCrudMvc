using System;
using System.Data.Entity;
using Database.EF.Entities;

namespace Database.EF
{
    /// <summary>
    /// Executes the strategy to initialize the database for the given context.
    /// </summary>
    public class DbInitializer : MigrateDatabaseToLatestVersion<MyContext, Database.EF.Migrations.Configuration>
    {
        /// <summary>
        /// Executes the strategy to initialize the database for the given context.
        /// </summary>
        /// <param name="context">DAtabase context.</param>
        /// <remarks>Create database for the first time. Do not check for updates.</remarks>
        public override void InitializeDatabase(MyContext context)
        {
            try
            {
                var databaseExists = context.Database.Exists();

                base.InitializeDatabase(context);

                if (!databaseExists)
                {
                    Seed(context);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Database could not be initialized.", ex);
            }
        }

        /// <summary>
        /// Seed creation.
        /// </summary>
        /// <param name="context">Database context.</param>
        protected void Seed(MyContext context)
        {
#if DEBUG
            AddCompanies(context);
#endif
        }

        /// <summary>
        /// Adds collection of companies to database.
        /// </summary>
        /// <param name="context">Database context.</param>
        protected void AddCompanies(MyContext context)
        {
            context.Users.Add(new User()
            {
                Name = "Joanna",
                Surname = "Lamch",
                Address = "Dabrowa gornicza",
                TelephoneNumber = "505502393"
            });
            context.SaveChanges();
        }
    }
}