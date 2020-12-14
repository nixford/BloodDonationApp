namespace BloodDonationApp.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BloodDonationApp.Common;
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
        public async Task CreateDonorCorrectly()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var userManager = this.GetUserManagerMock();

            var service = new DonorsService(donorDataRepository, usersRepository, rolesRepository);

            var input = new DonorDataProfileInputModel
            {
                Id = "123",
                UserName = "donor1",
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
                DonorAveilableStatus = (EmergencyStatus)1,
                ExaminationDate = DateTime.UtcNow,
                DiseaseName = "None",
            };

            await service.CreateDonorProfileAsync(input, input.Id);

            var user = usersRepository.All().Where(u => u.UserName == "donor1").FirstOrDefault();

            var isInRole = await userManager.Object.IsInRoleAsync(user, GlobalConstants.DonorRoleName);

            Assert.True(isInRole);

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
