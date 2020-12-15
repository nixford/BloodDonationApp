namespace BloodDonationApp.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Data.Repositories;
    using BloodDonationApp.Services.Data.Tests.InMemory;
    using BloodDonationApp.Web.ViewModels.Recipient;
    using Xunit;

    public class RecipientsServiceTest
    {
        [Fact]
        public async Task CreateRecipientNotNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            await SeedDataAsync(dbContext);

            var user = recipientRepository
                    .All()
                    .Where(u => u.Age == 85)
                    .FirstOrDefault();

            Assert.NotNull(user);
        }

        [Fact]
        public void CreateRecipientNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            var user = recipientRepository
                    .All()
                    .Where(u => u.Age == 85)
                    .FirstOrDefault();

            Assert.Null(user);
        }

        [Fact]
        public async Task CreateRecipientCorrectResipientHospitalDataEntityTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            await SeedDataAsync(dbContext);

            var recipient = recipientRepository
                    .All()
                    .Where(u => u.Age == 85)
                    .FirstOrDefault();

            var recipientHospitalData = recipientHospitalDataRepository
                    .All()
                    .FirstOrDefault();

            Assert.Equal(recipient.Id, recipientHospitalData.RecipientId);
        }

        [Fact]
        public async Task CreateRecipientShouldReturnNotNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            // Seeding user
            var user = new ApplicationUser
            {
                UserName = "User1",
                Email = "User1@user.bg",
            };
            dbContext.Users.Add(user);

            // Seeding hospital
            var hospital = new HospitalData
            {
                ApplicationUserId = user.Id,
                Name = "HospitalName",
                Contact = new Contact
                {
                    Phone = "123456789",
                    Email = "hospital@hospital.bg",
                },
                Location = new Location
                {
                    Country = "Bulgaria",
                    City = "Sofia",
                    AdressDescription = "Sofia",
                },
            };
            dbContext.HospitalData.Add(hospital);

            await dbContext.SaveChangesAsync();

            await service.AddRecipientAsync(hospital.ApplicationUserId, "recipient1", "recipient1", "recipient1", 85, 500, EmergencyStatus.WithinWeek, BloodGroup.A, RhesusFactor.Negative);

            var result = recipientRepository
                    .All()
                    .Where(u => u.Age == 85)
                    .FirstOrDefault();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateRecipientShouldthrowExceptionDueToNullHospitalDataTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            var result = recipientRepository
                    .All()
                    .Where(u => u.Age == 85)
                    .FirstOrDefault();

            await Assert.ThrowsAsync<ArgumentException>(() => service.AddRecipientAsync("1", "recipient1", "recipient1", "recipient1", 85, 500, EmergencyStatus.WithinWeek, BloodGroup.A, RhesusFactor.Negative));
        }

        [Fact]
        public async Task CreateRecipientShouldThrowExceptionIfInvalidDataIsPassTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            // Seeding user
            var user = new ApplicationUser
            {
                UserName = "User1",
                Email = "User1@user.bg",
            };
            dbContext.Users.Add(user);

            // Seeding hospital
            var hospital = new HospitalData
            {
                ApplicationUserId = user.Id,
                Name = "HospitalName",
                Contact = new Contact
                {
                    Phone = "123456789",
                    Email = "hospital@hospital.bg",
                },
                Location = new Location
                {
                    Country = "Bulgaria",
                    City = "Sofia",
                    AdressDescription = "Sofia",
                },
            };
            dbContext.HospitalData.Add(hospital);

            await dbContext.SaveChangesAsync();

            var result = recipientRepository
                    .All()
                    .Where(u => u.Age == 85)
                    .FirstOrDefault();

            await Assert.ThrowsAsync<ArgumentException>(() => service.AddRecipientAsync(hospital.ApplicationUserId, "recipient1", "recipient1", "recipient1", 85, 0, EmergencyStatus.WithinWeek, BloodGroup.A, RhesusFactor.Negative));
        }

        [Fact]
        public async Task AllHospitalRecipientsShouldReturnNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            await SeedDataAsync(dbContext);

            var result = service.AllHospitalRecipients<RecipientInfoViewModel>(null).Count();

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task AllHospitalRecipientsShouldReturnOneTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            await SeedDataAsync(dbContext);

            var hospital = hospitalDataRepository.All().FirstOrDefault();

            var result = service.AllHospitalRecipients<RecipientInfoViewModel>(hospital.ApplicationUserId).Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task TotalRecipientsShouldReturnOneTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            await SeedDataAsync(dbContext);

            var hospital = hospitalDataRepository.All().FirstOrDefault();

            var result = service.TotalRecipients<RecipientInfoViewModel>(hospital.ApplicationUserId).Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteAsyncRecipientsCorrectlyTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            await SeedDataAsync(dbContext);

            var hospital = hospitalDataRepository.All().FirstOrDefault();
            var recipient = recipientRepository.All().FirstOrDefault();

            await service.DeleteAsync(hospital.ApplicationUserId, recipient.Id);

            var result = service.TotalRecipients<RecipientInfoViewModel>(hospital.ApplicationUserId).Count();

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task DeleteAsyncRecipientsThrowsExceptionTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            await SeedDataAsync(dbContext);

            var hospital = hospitalDataRepository.All().FirstOrDefault();

            await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteAsync(hospital.ApplicationUserId, "1"));
        }

        [Fact]
        public async Task GetRecipientByIdShouldReturnCorrectlyTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            await SeedDataAsync(dbContext);

            var recipient = recipientRepository.All().FirstOrDefault();

            var result = service.GetById<RecipientInfoViewModel>(recipient.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetRecipientByIdShouldThrowErrorTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            await SeedDataAsync(dbContext);

            Assert.Throws<ArgumentException>(() => service.GetById<RecipientInfoViewModel>(null));
        }

        [Fact]
        public async Task AddRequestForRecipientCorrectlyTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            await service.AddRequestForRecipient("123", "456");

            var result = recipientReques.All().Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task AddRequestForRecipientThrowsExceptionTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var recipientHospitalDataRepository = new EfDeletableEntityRepository<RecipientHospitalData>(dbContext);

            var service = new RecipientsService(
                recipientRepository,
                hospitalDataRepository,
                recipientReques,
                recipientHospitalDataRepository);

            await service.AddRequestForRecipient("123", "456");

            await Assert.ThrowsAsync<ArgumentException>(() => service.AddRequestForRecipient("123", "456"));
        }

        private static async Task SeedDataAsync(ApplicationDbContext dbContext)
        {
            // Seeding user
            var user = new ApplicationUser
            {
                UserName = "User1",
                Email = "User1@user.bg",
            };
            dbContext.Users.Add(user);

            // Seeding hospital
            var hospital = new HospitalData
            {
                ApplicationUserId = user.Id,
                Name = "HospitalName",
                Contact = new Contact
                {
                    Phone = "123456789",
                    Email = "hospital@hospital.bg",
                },
                Location = new Location
                {
                    Country = "Bulgaria",
                    City = "Sofia",
                    AdressDescription = "Sofia",
                },
            };
            dbContext.HospitalData.Add(hospital);

            // Seeding recipient
            var recipient = new Recipient
            {
                HospitalDataId = hospital.Id,
                FirstName = "recipient1",
                MiddleName = "recipient1",
                LastName = "recipient1",
                Age = 85,
                NeededQuantity = 500,
                RecipientEmergency = EmergencyStatus.WithinWeek,
                BloodGroup = BloodGroup.A,
                RhesusFactor = RhesusFactor.Negative,
            };
            dbContext.Recipients.Add(recipient);

            // Seeding recipientHospital
            dbContext.RecipientHospitalData.Add(new RecipientHospitalData
            {
                HospitalDataId = hospital.Id,
                RecipientId = recipient.Id,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
