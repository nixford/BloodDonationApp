namespace BloodDonationApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Data.Repositories;
    using BloodDonationApp.Services.Data.Tests.InMemory;
    using BloodDonationApp.Web.ViewModels.Donor;
    using Xunit;

    public class DonationEventsServiceTest
    {
        [Fact]
        public async Task CreateDonationShouldReturnNotNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var donationEventRepository = new EfDeletableEntityRepository<DonationEvent>(dbContext);
            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);

            var service = new DonationEventsService(
                donationEventRepository,
                bloodBankRepository,
                bagRepository,
                hospitalDataRepository,
                donorDataRepository,
                requestRepository);

            await SeedDataAsync(dbContext);

            var donationEvent = donationEventRepository.All().FirstOrDefault(de => de.RequestId == "123");

            Assert.NotNull(donationEvent);
        }

        [Fact]
        public void CreateDonationShouldReturnNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var donationEventRepository = new EfDeletableEntityRepository<DonationEvent>(dbContext);
            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);

            var service = new DonationEventsService(
                donationEventRepository,
                bloodBankRepository,
                bagRepository,
                hospitalDataRepository,
                donorDataRepository,
                requestRepository);

            var donationEvent = donationEventRepository.All().FirstOrDefault(de => de.RequestId == "123");

            Assert.Null(donationEvent);
        }

        [Fact]
        public async Task CreateDonationCorectlyTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var donationEventRepository = new EfDeletableEntityRepository<DonationEvent>(dbContext);
            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);

            var service = new DonationEventsService(
                donationEventRepository,
                bloodBankRepository,
                bagRepository,
                hospitalDataRepository,
                donorDataRepository,
                requestRepository);


            var userDonor = new ApplicationUser
            {
                UserName = "User1",
                Email = "User1@gmail.com",
            };
            await dbContext.Users.AddAsync(userDonor);
            var donor = new DonorData
            {
                ApplicationUserId = userDonor.Id,
            };
            await dbContext.DonorData.AddAsync(donor);
            var userHospital = new ApplicationUser
            {
                UserName = "User2",
                Email = "User2@gmail.com",
            };
            await dbContext.Users.AddAsync(userDonor);

            var hospital = new HospitalData
            {
                ApplicationUserId = userHospital.Id,
            };
            await dbContext.HospitalData.AddAsync(hospital);

            var bloodBank = new BloodBank
            {
                HospitalDataId = hospital.Id,
            };
            await dbContext.BloodBanks.AddAsync(bloodBank);
            hospital.BloodBank = bloodBank;

            await dbContext.SaveChangesAsync();

            await service.CreateDonation(hospital.Id, donor.ApplicationUserId, 500, 0, BloodGroup.A, RhesusFactor.Negative);

            var donotionEvent = donationEventRepository.All().FirstOrDefault(de => de.UserDonorId == donor.ApplicationUserId);

            Assert.NotNull(donotionEvent);
        }

        [Fact]
        public async Task CreateDonationThrowNoDonorDataerrorTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var donationEventRepository = new EfDeletableEntityRepository<DonationEvent>(dbContext);
            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);

            var service = new DonationEventsService(
                donationEventRepository,
                bloodBankRepository,
                bagRepository,
                hospitalDataRepository,
                donorDataRepository,
                requestRepository);

            await SeedDataAsync(dbContext);

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateDonation("111", "222", 500, 0, BloodGroup.A, RhesusFactor.Negative));
        }

        [Fact]
        public async Task CreateDonationThrowNoHospitalDataerrorTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var donationEventRepository = new EfDeletableEntityRepository<DonationEvent>(dbContext);
            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);

            var service = new DonationEventsService(
                donationEventRepository,
                bloodBankRepository,
                bagRepository,
                hospitalDataRepository,
                donorDataRepository,
                requestRepository);

            await SeedDataAsync(dbContext);

            var serviceDonor = new DonorsService(donorDataRepository, usersRepository, rolesRepository);
            var user = usersRepository.All().FirstOrDefault();
            await serviceDonor.CreateDonorProfileAsync(this.SeedInputs(), user.Id);
            var donorData = donorDataRepository.All().FirstOrDefault(dd => dd.ApplicationUserId == user.Id);

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateDonation("111", donorData.ApplicationUserId, 500, 0, BloodGroup.A, RhesusFactor.Negative));
        }

        private static async Task SeedDataAsync(ApplicationDbContext dbContext)
        {
            // Seeding donationEvent
            var donationEvent = new DonationEvent
            {
                DateOfDonation = DateTime.UtcNow,
                RequestId = "123",
                UserDonorId = "456",
            };

            await dbContext.DonationEvents.AddAsync(donationEvent);

            // Seeding blood bag
            var bag = new BloodBag
            {
                Quantity = 500,
                CollectionDate = DateTime.UtcNow,
                BloodGroup = BloodGroup.A,
                RhesusFactor = RhesusFactor.Negative,
            };


            await dbContext.BloodBags.AddAsync(bag);
            await dbContext.SaveChangesAsync();

            // Seeding user for donor
            dbContext.Users.Add(new ApplicationUser
            {
                UserName = "User1",
                Email = "User1@user.bg",
            });

            // Seeding user for hospital
            dbContext.Users.Add(new ApplicationUser
            {
                UserName = "User2",
                Email = "User2@user.bg",
            });

            await dbContext.SaveChangesAsync();
        }

        private DonorDataProfileInputModel SeedInputs()
        {
            // Seeding input of donorData
            var input = new DonorDataProfileInputModel
            {
                Id = "123456",
                UserName = "User1",
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName",
                Age = 30,
                PhoneNumber = "123456789",
                BloodGroup = (BloodGroup)1,
                RhesusFactor = (RhesusFactor)1,
                Country = "Bulgaria",
                City = "Sofia",
                AdressDescription = "Sofia",
                DonorAveilableStatus = EmergencyStatus.AsSoonAsPossible,
                ExaminationDate = DateTime.UtcNow,
                DiseaseName = "None",
            };

            return input;
        }
    }
}
