namespace BloodDonationApp.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using BloodDonationApp.Common;
    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Services.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Seeding Admin
            var userAdmin = await userManager.FindByEmailAsync("Admin1@admin1.bg");
            if (userAdmin != null)
            {
                return;
            }

            userAdmin = new ApplicationUser
            {
                UserName = "Admin1@admin1.bg",
                Email = "Admin1@admin1.bg",
                EmailConfirmed = true,
            };
            await userManager.CreateAsync(userAdmin, "Admin1@admin1.bg");
            await userManager.AddToRoleAsync(userAdmin, GlobalConstants.AdministratorRoleName);
        }
    }
}
