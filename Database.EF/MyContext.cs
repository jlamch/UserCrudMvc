using System.Data.Entity;
using Database.EF.Entities;

namespace Database.EF
{
    /// <summary>
    /// Database context and CodeFirst data schema.
    /// </summary>
    public class MyContext : DbContext, IMyContext
    {
        /// <summary>
        /// Default connection string configuration name.
        /// </summary>
        public const string ConnectionStringName = "DefaultConnectionString";

        /// <summary>
        /// Initializes a new instance of the MyContext class.
        /// </summary>
        public MyContext()
            : base(ConnectionStringName)
        {
        }

        /// Gets or sets collection of users.
        /// </summary>
        /// <value>Collection of users.</value>
        public IDbSet<User> Users { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized,
        ///  but before the model has been locked down and used to initialize the context.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}