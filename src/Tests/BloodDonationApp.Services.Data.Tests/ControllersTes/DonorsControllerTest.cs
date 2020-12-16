namespace BloodDonationApp.Services.Data.Tests.ControllersTes
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Web.Controllers;
    using BloodDonationApp.Web.ViewModels.Donor;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class DonorsControllerTest
    {
        [Fact]
        public void AddDonorHttpGetShouldReturnNotFoundResultInCaseOfEmptyViewModelTest()
        {
            var mockUserManagerService = this.GetUserManagerMock();
            var mockDonorsService = new Mock<IDonorsService>();
            var mockUsersService = new Mock<IUsersService>();

            var controller = new DonorsController(
                mockUserManagerService.Object,
                mockDonorsService.Object,
                mockUsersService.Object);

            var result = controller.AddDonor();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AddDonorHttpGetShouldReturnViewModelTest()
        {
            var mockUserManagerService = this.GetUserManagerMock();
            var mockDonorsService = new Mock<IDonorsService>();
            var mockUsersService = new Mock<IUsersService>();
            mockUsersService.Setup(x => x.GetUserById<DonorDataProfileInputModel>("123"))
                .Returns(new DonorDataProfileInputModel()
            {
                Id = "123",
                FirstName = "Donor1",
                LastName = "Donor1",
            });

            var controller = new DonorsController(
                mockUserManagerService.Object,
                mockDonorsService.Object,
                mockUsersService.Object);

            var result = controller.AddDonor();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AddDonorHttpPostInvalidInputShouldThrowAnyExceptionTest()
        {
            var mockUserManagerService = this.GetUserManagerMock();
            var mockDonorsService = new Mock<IDonorsService>();
            var mockUsersService = new Mock<IUsersService>();
            var input = new DonorDataProfileInputModel();

            var controller = new DonorsController(
                mockUserManagerService.Object,
                mockDonorsService.Object,
                mockUsersService.Object);

            Assert.ThrowsAnyAsync<Exception>(() => controller.AddDonor(input));
        }

        [Fact]
        public void AddDonorHttpPostValidInputShouldRedirectResultTest()
        {
            var mockUserManagerService = this.GetUserManagerMock();
            var mockDonorsService = new Mock<IDonorsService>();
            var mockUsersService = new Mock<IUsersService>();

            var input = this.SeedData();

            var controller = new DonorsController(
                mockUserManagerService.Object,
                mockDonorsService.Object,
                mockUsersService.Object);

            var result = controller.AddDonor(input);
            Assert.IsType<RedirectToActionResult>(result.Result);
        }

        [Fact]
        public void AddDonorHttpPostValidInputShouldRedirectToCorrectActionAndCotrollerTest()
        {
            var mockUserManagerService = this.GetUserManagerMock();
            var mockDonorsService = new Mock<IDonorsService>();
            var mockUsersService = new Mock<IUsersService>();

            var controller = new DonorsController(
                mockUserManagerService.Object,
                mockDonorsService.Object,
                mockUsersService.Object);

            var input = this.SeedData();

            var result = controller.AddDonor(input).Result;

            var redirect = result as RedirectToActionResult;
            Assert.Equal($"AllRequests", redirect.ActionName);
            Assert.Equal($"Requests", redirect.ControllerName);
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
            userManagerMock.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns("123");

            return userManagerMock;
        }

        private DonorDataProfileInputModel SeedData()
        {
            var input = new DonorDataProfileInputModel
            {
                Id = "123",
                FirstName = "Donor1",
                MiddleName = "Donor1",
                LastName = "Donor1",
                Age = 30,
                PhoneNumber = "123456789",
                BloodGroup = BloodGroup.A,
                RhesusFactor = RhesusFactor.Negative,
                Country = "Bulgaria",
                City = "Sofia",
                AdressDescription = "Sofia",
                DonorAveilableStatus = EmergencyStatus.WithinDay,
                ExaminationDate = DateTime.UtcNow,
                DiseaseName = "none",
            };

            return input;
        }
    }
}
