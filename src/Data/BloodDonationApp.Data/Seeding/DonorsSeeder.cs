namespace BloodDonationApp.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Common;
    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class DonorsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var donorDataRepository = serviceProvider.GetService<IRepository<DonorData>>();
            var allUsers = serviceProvider.GetService<IDeletableEntityRepository<ApplicationUser>>();
            var deletedUsers = allUsers
                .AllAsNoTrackingWithDeleted()
                .Where(u => u.IsDeleted == true)
                .ToList();

            // Seeding donors and donorsData
            for (int i = 0; i < 10; i++)
            {
                var user = await userManager.FindByEmailAsync($"Donor{i}@donor.bg");
                if (user != null)
                {
                    return;
                }

                user = new ApplicationUser
                {
                    UserName = $"Donor{i}@donor.bg",
                    Email = $"Donor{i}@donor.bg",
                    EmailConfirmed = true,
                };

                if (deletedUsers.Any(u => u.Email == user.Email))
                {
                    return;
                }

                await userManager.CreateAsync(user, $"Donor{i}@donor.bg");
                await userManager.AddToRoleAsync(user, GlobalConstants.DonorRoleName);

                Random random = new Random();
                var donorData = new DonorData
                {
                    ApplicationUserId = user.Id,
                    ApplicationUser = user,
                    FirstName = $"Donor{i}",
                    MiddleName = $"Donor{i}",
                    LastName = $"Donor{i}",
                    Age = 20 + i,
                    Contact = new Contact
                    {
                        Email = user.Email,
                        Phone = $"0{i * 2}{i * 3}{i * 4}",
                    },
                    Location = new Location
                    {
                        Country = "Bulgaria",
                        City = $"City{i}",
                        AdressDescription = $"Street{i}",
                    },
                    DonorAveilableStatus = (EmergencyStatus)random.Next(Enum.GetNames(typeof(EmergencyStatus)).Length),
                    BloodGroup = (BloodGroup)random.Next(Enum.GetNames(typeof(BloodGroup)).Length),
                    RhesusFactor = (RhesusFactor)random.Next(Enum.GetNames(typeof(RhesusFactor)).Length),
                    Examination = new Examination
                    {
                        ExaminationDate = DateTime.UtcNow,
                        Disease = new Disease
                        {
                            DiseaseName = "NoDesease",
                            DiseaseDescription = "NoDescription",
                            DiseaseStatus = DiseaseStatus.Negative,
                        },
                    },
                };
                await donorDataRepository.AddAsync(donorData);
            }
        }
    }
}
