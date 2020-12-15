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
    using BloodDonationApp.Web.ViewModels.Hospital;
    using Xunit;

    public class BloodBanksServiceTest
    {
        [Fact]
        public async Task CreateBloodBankShouldReturnNotNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);

            var service = new BloodBanksService(
                bloodBankRepository,
                bagRepository,
                hospitalDataRepository);

            await SeedDataAsync(dbContext);

            var bloodBank = bloodBankRepository.All().FirstOrDefault();

            Assert.NotNull(bloodBank);
        }

        [Fact]
        public void CreateDonationShouldReturnNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);

            var service = new BloodBanksService(
                bloodBankRepository,
                bagRepository,
                hospitalDataRepository);

            var bloodBank = bloodBankRepository.All().FirstOrDefault();

            Assert.Null(bloodBank);
        }

        [Fact]
        public async Task GetHospitalsBloodBagsByHospitalIdShouldReturnOneTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);

            var service = new BloodBanksService(
                bloodBankRepository,
                bagRepository,
                hospitalDataRepository);

            await SeedDataAsync(dbContext);

            var bloodBanks = service.GetHospitalBloodBagsById("123").Count();

            Assert.Equal(1, bloodBanks);
        }

        [Fact]
        public void GetHospitalsBloodBagsByHospitalIdShouldThrowExceptionTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);

            var service = new BloodBanksService(
                bloodBankRepository,
                bagRepository,
                hospitalDataRepository);

            Assert.Throws<ArgumentException>(() => service.GetHospitalBloodBagsById("123"));
        }

        [Fact]
        public async Task GetHospitalsBloodBagsByHospitalIdShouldReturnCorrectBloodBagTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);

            var service = new BloodBanksService(
                bloodBankRepository,
                bagRepository,
                hospitalDataRepository);

            await SeedDataAsync(dbContext);

            var bloodBanks = service.GetHospitalBloodBagsById("123").ToList();

            Assert.Equal(500, bloodBanks[0].Quantity);
        }

        private static async Task SeedDataAsync(ApplicationDbContext dbContext)
        {
            // Seeding blood bank
            var bloodBank = new BloodBank
            {
                HospitalDataId = "123",
            };
            dbContext.BloodBanks.Add(bloodBank);
            await dbContext.SaveChangesAsync();

            // Seeding blood bag
            var bag = new BloodBag
            {
                Quantity = 500,
                CollectionDate = DateTime.UtcNow,
                DonorDataId = "789",
                BloodGroup = BloodGroup.A,
                RhesusFactor = RhesusFactor.Negative,
                BloodBankId = bloodBank.Id,
            };
            await dbContext.BloodBags.AddAsync(bag);
            await dbContext.SaveChangesAsync();
        }
    }
}
