namespace BloodDonationApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Common;
    using BloodDonationApp.Data;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Data.Repositories;
    using BloodDonationApp.Services.Data.Tests.InMemory;
    using BloodDonationApp.Web.ViewModels.Donor;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Xunit;

    public class DonorsServiceTest
    {
        [Fact]
        public async Task CreateDonorUserIsNotNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var userManager = this.GetUserManagerMock();

            var service = new DonorsService(donorDataRepository, usersRepository, rolesRepository);

            await SeedDataAsync(dbContext);

            await service.CreateDonorProfileAsync(this.SeedInputs()[0], this.SeedInputs()[0].Id);

            var user = usersRepository.All().Where(u => u.UserName == "User1").FirstOrDefault();

            Assert.NotNull(user);
        }

        [Fact]
        public async Task CreateDonorIsInCorrectRoleTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var userManager = this.GetUserManagerMock();

            var service = new DonorsService(donorDataRepository, usersRepository, rolesRepository);

            await SeedDataAsync(dbContext);

            await service.CreateDonorProfileAsync(this.SeedInputs()[0], this.SeedInputs()[0].Id);

            var user = usersRepository.All().Where(u => u.UserName == "User1").FirstOrDefault();

            var isInRole = await userManager.Object.IsInRoleAsync(user, GlobalConstants.DonorRoleName);

            Assert.True(isInRole);
        }

        [Fact]
        public async Task CreateDonorThrowsExceptionUserShouldBeRegisteredFirstTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var userManager = this.GetUserManagerMock();

            var service = new DonorsService(donorDataRepository, usersRepository, rolesRepository);

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateDonorProfileAsync(this.SeedInputs()[0], this.SeedInputs()[0].Id));
        }

        [Fact]
        public async Task GetAllDonorsTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var userManager = this.GetUserManagerMock();

            var service = new DonorsService(donorDataRepository, usersRepository, rolesRepository);

            await SeedDataAsync(dbContext);

            await service.CreateDonorProfileAsync(this.SeedInputs()[0], this.SeedInputs()[0].Id);

            var donors = donorDataRepository.All().Count();

            Assert.Equal(1, donors);
        }

        private static async Task SeedDataAsync(ApplicationDbContext dbContext)
        {
            // Seeding user
            dbContext.Users.Add(new ApplicationUser
            {
                Id = "123",
                UserName = "User1",
                Email = "User1@user.bg",
            });
            await dbContext.SaveChangesAsync();
        }

        private IList<DonorDataProfileInputModel> SeedInputs()
        {
            var inputs = new List<DonorDataProfileInputModel>();

            // Seeding input of donorData
            var input = new DonorDataProfileInputModel
            {
                Id = "123",
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
            inputs.Add(input);

            for (int i = 0; i < 5; i++)
            {
                var inputLoop = new DonorDataProfileInputModel
                {
                    Id = $"123{i}",
                    UserName = $"User1{i}",
                    FirstName = $"FirstName{i}",
                    MiddleName = $"MiddleName{i}",
                    LastName = $"LastName{i}",
                    Age = 30,
                    PhoneNumber = $"123456789{i}",
                    BloodGroup = (BloodGroup)1,
                    RhesusFactor = (RhesusFactor)1,
                    Country = "Bulgaria",
                    City = "Sofia",
                    AdressDescription = "Sofia",
                    DonorAveilableStatus = EmergencyStatus.WithinDay,
                    ExaminationDate = DateTime.UtcNow,
                    DiseaseName = "None",
                };
                inputs.Add(inputLoop);
            }

            return inputs;
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
