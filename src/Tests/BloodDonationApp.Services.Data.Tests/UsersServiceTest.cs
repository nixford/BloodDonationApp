namespace BloodDonationApp.Services.Data.Tests.Administration.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BloodDonationApp.Data;
    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class UsersServiceTest
    {
        [Fact]
        public async Task GetNumberOfAdministratorsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var roleStore = new Mock<IRoleStore<ApplicationRole>>();

            var roleManagerMock =
                new Mock<RoleManager<ApplicationRole>>(roleStore.Object, null, null, null, null);
            roleManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationRole { Id = Guid.NewGuid().ToString(), Name = "Administrator" });

            using (var db = new ApplicationDbContext(options))
            {
                IUsersService usersService = new UsersService(null, null, null, null, null, null, null);
                var result = usersService.GetAllAdmins().Count();

                Assert.Equal(0, result);
            }
        }


        private UsersService InitializeService(
            IDeletableEntityRepository<ApplicationUser>? userRepository,
            IDeletableEntityRepository<ApplicationRole>? roleRepository,
            IDeletableEntityRepository<HospitalData>? hospitalDataRepository,
            IDeletableEntityRepository<DonorData>? donorDataRepository,
            IDeletableEntityRepository<Recipient>? recipientsRepository,
            IDeletableEntityRepository<Request>? requestsRepository)
        {
            var userManager = this.GetUserManagerMock();

            var service = new UsersService(
                userRepository, 
                userManager.Object,
                roleRepository,
                hospitalDataRepository,
                donorDataRepository,
                recipientsRepository,
                requestsRepository);

            return service;
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
