namespace BloodDonationApp.Services.Data.Tests.Common
{
    using System;

    using BloodDonationApp.Data;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContextInMemoryFactory
    {
        public static ApplicationDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            dbContext.Database.EnsureDeleted();
            return dbContext;
        }
    }
}
