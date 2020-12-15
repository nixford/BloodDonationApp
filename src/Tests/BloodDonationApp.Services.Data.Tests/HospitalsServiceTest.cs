namespace BloodDonationApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Common;
    using BloodDonationApp.Data;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Repositories;
    using BloodDonationApp.Services.Data.Tests.InMemory;
    using BloodDonationApp.Web.ViewModels.Hospital;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Xunit;

    public class HospitalsServiceTest
    {
        [Fact]
        public async Task CreateHospitalUserIsNotNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var appUsersHospitalRepository = new EfDeletableEntityRepository<ApplicationUserHospitalData>(dbContext);
            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var hospitalBloodBankRepository = new EfDeletableEntityRepository<HospitalDataBloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);

            var userManager = this.GetUserManagerMock();

            var service = new HospitalsService(
                usersRepository,
                hospitalDataRepository,
                rolesRepository,
                appUsersHospitalRepository,
                recipientRepository,
                bloodBankRepository,
                hospitalBloodBankRepository,
                bagRepository);

            await SeedDataAsync(dbContext);

            await service.CreateHospitalProfileAsync(this.SeedInputs(), this.SeedInputs().Id);

            var user = usersRepository.All().Where(u => u.UserName == "User1").FirstOrDefault();

            Assert.NotNull(user);
        }

        [Fact]
        public async Task CreateHospitalUserIsInCorrectRoleTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var appUsersHospitalRepository = new EfDeletableEntityRepository<ApplicationUserHospitalData>(dbContext);
            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var hospitalBloodBankRepository = new EfDeletableEntityRepository<HospitalDataBloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);

            var userManager = this.GetUserManagerMock();

            var service = new HospitalsService(
                usersRepository,
                hospitalDataRepository,
                rolesRepository,
                appUsersHospitalRepository,
                recipientRepository,
                bloodBankRepository,
                hospitalBloodBankRepository,
                bagRepository);

            await SeedDataAsync(dbContext);

            await service.CreateHospitalProfileAsync(this.SeedInputs(), this.SeedInputs().Id);

            var user = usersRepository.All().Where(u => u.UserName == "User1").FirstOrDefault();

            var isInRole = await userManager.Object.IsInRoleAsync(user, GlobalConstants.HospitaltRoleName);

            Assert.True(isInRole);
        }

        [Fact]
        public async Task CreateHospitalThrowsExceptionUserShouldBeRegisteredFirstTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var appUsersHospitalRepository = new EfDeletableEntityRepository<ApplicationUserHospitalData>(dbContext);
            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var hospitalBloodBankRepository = new EfDeletableEntityRepository<HospitalDataBloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);

            var service = new HospitalsService(
                usersRepository,
                hospitalDataRepository,
                rolesRepository,
                appUsersHospitalRepository,
                recipientRepository,
                bloodBankRepository,
                hospitalBloodBankRepository,
                bagRepository);

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateHospitalProfileAsync(this.SeedInputs(), this.SeedInputs().Id));
        }

        [Fact]
        public async Task GetAllHospitalsShouldBeOneTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var appUsersHospitalRepository = new EfDeletableEntityRepository<ApplicationUserHospitalData>(dbContext);
            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var hospitalBloodBankRepository = new EfDeletableEntityRepository<HospitalDataBloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);

            var userManager = this.GetUserManagerMock();

            var service = new HospitalsService(
                usersRepository,
                hospitalDataRepository,
                rolesRepository,
                appUsersHospitalRepository,
                recipientRepository,
                bloodBankRepository,
                hospitalBloodBankRepository,
                bagRepository);

            await SeedDataAsync(dbContext);
            await service.CreateHospitalProfileAsync(this.SeedInputs(), this.SeedInputs().Id);

            var result = service.GetAllHospitals<HospitalProfileInputModel>().Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetHospitalByUserIdShouldReturnNotNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var appUsersHospitalRepository = new EfDeletableEntityRepository<ApplicationUserHospitalData>(dbContext);
            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var hospitalBloodBankRepository = new EfDeletableEntityRepository<HospitalDataBloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);

            var userManager = this.GetUserManagerMock();

            var service = new HospitalsService(
                usersRepository,
                hospitalDataRepository,
                rolesRepository,
                appUsersHospitalRepository,
                recipientRepository,
                bloodBankRepository,
                hospitalBloodBankRepository,
                bagRepository);

            await SeedDataAsync(dbContext);
            await service.CreateHospitalProfileAsync(this.SeedInputs(), this.SeedInputs().Id);

            var result = service.GetHospitalDataById<HospitalProfileInputModel>("123");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetHospitalByUserIdShouldReturnNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var appUsersHospitalRepository = new EfDeletableEntityRepository<ApplicationUserHospitalData>(dbContext);
            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var bloodBankRepository = new EfDeletableEntityRepository<BloodBank>(dbContext);
            var hospitalBloodBankRepository = new EfDeletableEntityRepository<HospitalDataBloodBank>(dbContext);
            var bagRepository = new EfDeletableEntityRepository<BloodBag>(dbContext);

            var userManager = this.GetUserManagerMock();

            var service = new HospitalsService(
                usersRepository,
                hospitalDataRepository,
                rolesRepository,
                appUsersHospitalRepository,
                recipientRepository,
                bloodBankRepository,
                hospitalBloodBankRepository,
                bagRepository);

            await SeedDataAsync(dbContext);
            await service.CreateHospitalProfileAsync(this.SeedInputs(), this.SeedInputs().Id);

            var result = service.GetHospitalDataById<HospitalProfileInputModel>("111");

            Assert.Null(result);
        }

        private static async Task SeedDataAsync(ApplicationDbContext dbContext)
        {
            // Seeding user with id
            dbContext.Users.Add(new ApplicationUser
            {
                Id = "123",
                UserName = "User1",
                Email = "User1@user.bg",
            });
            await dbContext.SaveChangesAsync();
        }

        private HospitalProfileInputModel SeedInputs()
        {
            // Seeding input of hospitalData
            var input = new HospitalProfileInputModel
            {
                Id = "123",
                UserName = "User1",
                Name = "HospitalName",
                Phone = "123456789",
                Email = "hospital@hospital.bg",
                Country = "Bulgaria",
                City = "Sofia",
                AdressDescription = "Sofia",
            };

            return input;
        }

        private Mock<UserManager<ApplicationUser>> GetUserManagerMock()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            userManagerMock
                .Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            return userManagerMock;
        }
    }
}
