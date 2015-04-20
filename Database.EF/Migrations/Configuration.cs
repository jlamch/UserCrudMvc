using System.Data.Entity.Migrations;

namespace Database.EF.Migrations
{
    /// <summary>
    /// Configuration.
    /// </summary>
    public sealed class Configuration : DbMigrationsConfiguration<Database.EF.MyContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(Database.EF.MyContext context)
        {
        }

        #region migration help
        //// get-help Enable-Migrations -detailed
        //// get-help Add-Migration -detailed
        //// get-help Update-Database -detailed
        //// get-help Get-Migrations -detailed
        //// Enable-Migrations [-EnableAutomaticMigrations] [[-ProjectName] <String>]  [-Force] [<CommonParameters>]
        //// Update-Database -Script -SourceMigration: $InitialDatabase -TargetMigration: UserUpdateDate -Verbose
        //// Update-Database -Script -SourceMigration: UserUpdateDate -TargetMigration: AppSpecific -Verbose
        //// Add-Migration -Name -IgnoreChanges -Project AppSpecific
        //// MSroka
        //// Enable-Migrations -Project SMT.Loft.Ems.Database.EF
        //// Add-Migration InitialCreate -Project SMT.Loft.Ems.Database.EF
        //// Update-Database -Project SMT.Loft.Ems.Database.EF
        //// Add-Migration IPadCreateAccount -Project SMT.Loft.Ems.Database.EF
        //// Update-Database -Project SMT.Loft.Ems.Database.EF
        #endregion
    }
}