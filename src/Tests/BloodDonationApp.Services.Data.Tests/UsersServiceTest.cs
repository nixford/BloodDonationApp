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
        public void GetNumberOfAdministratorsShouldBeZeroTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var userManager = this.GetUserManagerMock();
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);

            var result = usersRepository.All().Count();

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetNumberOfAdministratorsShouldBeNotNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var userManager = this.GetUserManagerMock();
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var recipientRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);

            await SeedDataAsync(dbContext);

            var result = usersRepository.All().FirstOrDefault();

            Assert.NotNull(result);
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
        public async Task GetUserByIdShouldReturnSameUserTest()
        {
            MapperInitializer.InitializeMapper();
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);

            await SeedDataAsync(dbContext);
            var user = usersRepository.All().FirstOrDefault();

            var result = service.GetUserById<DonorDataProfileInputModel>(user.Id);

            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public void GetUserByIdThrowsExceptionIfIdIsNullTest()
        {
            MapperInitializer.InitializeMapper();
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);

            Assert.Throws<ArgumentException>(() => service.GetUserById<DonorDataProfileInputModel>(null));
        }

        [Fact]
        public async Task GetUserByIdShouldReturnNullTest()
        {
            MapperInitializer.InitializeMapper();
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);
            string id = "1234";

            await SeedDataAsync(dbContext);

            var result = service.GetUserById<DonorDataProfileInputModel>(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByNameShouldReturnSameUserTest()
        {
            MapperInitializer.InitializeMapper();
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);

            await SeedDataAsync(dbContext);
            var user = usersRepository.All().FirstOrDefault();

            var result = service.GetUserByName<DonorDataProfileInputModel>(user.UserName);

            Assert.Equal(user.UserName, result.UserName);
        }

        [Fact]
        public async Task GetUserByNameShouldReturnZeroTest()
        {
            MapperInitializer.InitializeMapper();
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);
            string username = "User5";

            await SeedDataAsync(dbContext);

            var result = service.GetUserByName<DonorDataProfileInputModel>(username);

            Assert.Null(result);
        }

        [Fact]
        public void GetUserByNameThrowsExceptionIfNameIdNullTest()
        {
            MapperInitializer.InitializeMapper();
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);

            Assert.Throws<ArgumentException>(() => service.GetUserByName<DonorDataProfileInputModel>(null));
        }

        [Fact]
        public async Task GetUserEmailShouldReturnCorrectEmailTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);
            string id = "123";

            await SeedDataAsync(dbContext);

            var result = service.GetUserEmailById(id);

            Assert.Equal("User2@user.bg", result);
        }

        [Fact]
        public async Task GetUserEmailShouldReturnNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);
            string id = "1234";

            await SeedDataAsync(dbContext);

            var result = service.GetUserEmailById(id);

            Assert.Null(result);
        }

        [Fact]
        public void GetUserEmailThrowsExceptionIfIdIsNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);

            Assert.Throws<ArgumentException>(() => service.GetUserEmailById(null));
        }

        [Fact]
        public async Task AddAdminCorrectlyTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);
            string userName = "Admin1@admin.bg";
            string email = "Admin1@admin.bg";
            string password = "password";

            var result = service.AddAdmin(userName, email, password);

            var user = usersRepository.All().Where(u => u.UserName == "Admin1@admin.bg").FirstOrDefault();

            var isInRole = await userManager.Object.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);

            Assert.True(isInRole);
        }

        [Fact]
        public async Task AddAdminThrowExceptionIfUsernameExistTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);

            await SeedDataAsync(dbContext);

            string userName = "Admin";
            string email = "admin1@admin1.bg";
            string password = "password";

            await Assert.ThrowsAsync<ArgumentException>(() => service.AddAdmin(userName, email, password));
        }

        [Fact]
        public async Task AddAdminThrowExceptionIfEmailExistTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, null, null, null, null);

            await SeedDataAsync(dbContext);

            string userName = "Admin123";
            string email = "Admin@admin.bg";
            string password = "password";

            await Assert.ThrowsAsync<ArgumentException>(() => service.AddAdmin(userName, email, password));
        }

        [Fact]
        public async Task RemoveUserCorrectlyTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var recipientDataRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var requestDataRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, hospitalDataRepository, donorDataRepository, recipientDataRepository, requestDataRepository);

            await SeedDataAsync(dbContext);

            await service.RemoveUserAsync("Admin@admin.bg");

            var result = usersRepository.All().Where(u => u.UserName == "Admin").FirstOrDefault();

            Assert.Null(result);
        }

        [Fact]
        public async Task RemoveUserThrowsEceptionIfUserIsMissingTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var recipientDataRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var requestDataRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(usersRepository, userManager.Object, rolesRepository, hospitalDataRepository, donorDataRepository, recipientDataRepository, requestDataRepository);

            await SeedDataAsync(dbContext);

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.RemoveUserAsync("Admin1@admin.bg"));
        }

        [Fact]
        public async Task RemoveUserThrowsEceptionIfEmailIsNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var donorDataRepository = new EfDeletableEntityRepository<DonorData>(dbContext);
            var recipientDataRepository = new EfDeletableEntityRepository<Recipient>(dbContext);
            var requestDataRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var userManager = this.GetUserManagerMock();
            var rolesRepository = new EfDeletableEntityRepository<ApplicationRole>(dbContext);

            var service = new UsersService(
                usersRepository,
                userManager.Object,
                rolesRepository,
                hospitalDataRepository,
                donorDataRepository,
                recipientDataRepository,
                requestDataRepository);

            await SeedDataAsync(dbContext);

            await Assert.ThrowsAsync<ArgumentException>(() => service.RemoveUserAsync(null));
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
