namespace BloodDonationApp.Services.Data.Tests.Administration.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Common;
    using BloodDonationApp.Data;
    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Repositories;
    using BloodDonationApp.Data.Seeding;
    using BloodDonationApp.Services.Data.Tests.InMemory;
    using BloodDonationApp.Web.ViewModels.Donor;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Xunit;

    public class UsersServiceTest
    {
        [Fact]
        public async Task GetNumberOfAdministratorsMustBeZeroTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);

            await SeedDataAsync(dbContext);

            var result = service.GetAllAdmins().Count();

            Assert.Equal(0, result - 1);
        }

        [Fact]
        public async Task GetNumberOfAdministratorsMustBeOneAfterSeedingTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);

            await SeedDataAsync(dbContext);

            var result = service.GetAllAdmins().Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetAllDeletedUsersShouldBeOneTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);

            await SeedDataAsync(dbContext);

            var result = service.GetAllDeletedUsers().Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetUserByIdShouldReturnUserTest()
        {
            MapperInitializer.InitializeMapper();
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);
            string id = "123";

            await SeedDataAsync(dbContext);

            var result = service.GetUserById<DonorDataProfileInputModel>(id);

            Assert.Equal("123", result.Id);
        }

        private static async Task SeedDataAsync(ApplicationDbContext dbContext)
        {
            // Seeding admin
            var user = new ApplicationUser
            {
                UserName = "Admin",
                Email = "Admin@admin.bg",
            };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            var roles = new ApplicationRole
            {
                Name = GlobalConstants.AdministratorRoleName,
            };
            dbContext.Roles.Add(roles);
            await dbContext.SaveChangesAsync();
            dbContext.UserRoles.Add(new IdentityUserRole<string>
            {
                UserId = user.Id,
                RoleId = roles.Id,
            });
            await dbContext.SaveChangesAsync();

            // Seeding deleted user
            dbContext.Users.Add(new ApplicationUser
            {
                UserName = "User1",
                Email = "User1@user.bg",
                IsDeleted = true,
            });
            await dbContext.SaveChangesAsync();

            // Seeding user with id
            dbContext.Users.Add(new ApplicationUser
            {
                Id = "123",
                UserName = "User2",
                Email = "User2@user.bg",
            });
            await dbContext.SaveChangesAsync();
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
