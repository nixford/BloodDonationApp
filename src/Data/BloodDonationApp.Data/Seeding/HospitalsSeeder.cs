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

    public class HospitalsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var hospitalDataRepository = serviceProvider.GetRequiredService<IDeletableEntityRepository<HospitalData>>();
            var recipientsRepository = serviceProvider.GetRequiredService<IDeletableEntityRepository<Recipient>>();
            var recipientsHospitalDataRepository = serviceProvider.GetRequiredService<IDeletableEntityRepository<RecipientHospitalData>>();
            var bloodBankRepository = serviceProvider.GetRequiredService<IDeletableEntityRepository<BloodBank>>();
            var hospitalBloodBankRepository = serviceProvider.GetRequiredService<IDeletableEntityRepository<HospitalDataBloodBank>>();
            var allUsers = serviceProvider.GetService<IDeletableEntityRepository<ApplicationUser>>();
            var deletedUsers = allUsers
                .AllAsNoTrackingWithDeleted()
                .Where(u => u.IsDeleted == true)
                .ToList();


            // Seeding hospitals and hospitalsData
            for (int i = 0; i < 5; i++)
            {
                var user = await userManager.FindByEmailAsync($"Hospital{i}@hospital.bg");
                if (user != null)
                {
                    return;
                }

                user = new ApplicationUser
                {
                    UserName = $"Hospital{i}@hospital.bg",
                    Email = $"Hospital{i}@hospital.bg",
                    EmailConfirmed = true,
                };

                if (deletedUsers.Any(u => u.Email == user.Email))
                {
                    return;
                }

                await userManager.CreateAsync(user, $"Hospital{i}@hospital.bg");
                await userManager.AddToRoleAsync(user, GlobalConstants.HospitaltRoleName);

                var hospitalData = new HospitalData
                {
                    ApplicationUserId = user.Id,
                    ApplicationUser = user,
                    Name = $"Hospital{i}",
                    Contact = new Contact
                    {
                        Email = user.Email,
                        Phone = $"0{i * 5}{i * 6}{i * 7}",
                    },
                    Location = new Location
                    {
                        Country = "Bulgaria",
                        City = $"City{i}",
                        AdressDescription = $"Street{i * 3}",
                    },
                };
                await hospitalDataRepository.AddAsync(hospitalData);

                var bloodBank = new BloodBank
                {
                    HospitalDataId = hospitalData.Id,
                };
                await bloodBankRepository.AddAsync(bloodBank);

                var hospitalBloodBank = new HospitalDataBloodBank
                {
                    HospitalId = hospitalData.Id,
                    BloodBankId = bloodBank.Id,
                };
                await hospitalBloodBankRepository.AddAsync(hospitalBloodBank);

                // Seeding recipients in hospitals
                for (int j = 0; j < 3; j++)
                {
                    Random random = new Random();
                    var recipient = new Recipient
                    {
                        HospitalDataId = hospitalData.Id,
                        FirstName = $"Recipient{j}",
                        MiddleName = $"Recipient{j}",
                        LastName = $"Recipient{j}",
                        Age = 20 + j,
                        NeededQuantity = 500,
                        RecipientEmergency = (EmergencyStatus)random.Next(Enum.GetNames(typeof(EmergencyStatus)).Length),
                        BloodGroup = (BloodGroup)random.Next(Enum.GetNames(typeof(BloodGroup)).Length),
                        RhesusFactor = (RhesusFactor)random.Next(Enum.GetNames(typeof(RhesusFactor)).Length),
                    };

                    await recipientsRepository.AddAsync(recipient);

                    var recipientHospitalData = new RecipientHospitalData
                    {
                        HospitalDataId = hospitalData.Id,
                        RecipientId = recipient.Id,
                    };

                    await recipientsHospitalDataRepository.AddAsync(recipientHospitalData);
                }
            }
        }
    }
}
