namespace Database.Domain.Repositories
{
    /// <summary>
    /// Access to initlialize datbase.
    /// </summary>
    public interface IInitializeDatabase
    {
        /// <summary>
        /// Execute the strategy to initialize the database.
        /// </summary>
        void Initialize();
    }
}