namespace BloodDonationApp.Services.Data.Tests.ControllersTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Web.Controllers;
    using BloodDonationApp.Web.ViewModels.BloodBank;
    using BloodDonationApp.Web.ViewModels.Hospital;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class HospitalsControllerTest
    {
        [Fact]
        public void AddHospitalHttpGetShouldReturnNotFoundResultInCaseOfEmptyViewModelTest()
        {
            var mockHospitalsService = new Mock<IHospitalsService>();
            var mockUserManagerService = this.GetUserManagerMock();
            var mockRecipientsService = new Mock<IRecipientsService>();
            var mockBloodbanksService = new Mock<IBloodBanksService>();
            var mockUsersService = new Mock<IUsersService>();

            var controller = new HospitalsController(
                mockHospitalsService.Object,
                mockUserManagerService.Object,
                mockRecipientsService.Object,
                mockBloodbanksService.Object,
                mockUsersService.Object);

            var result = controller.AddHospital();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AddHospitalHttpGetShouldReturnViewModelTest()
        {
            var mockHospitalsService = new Mock<IHospitalsService>();
            var mockUserManagerService = this.GetUserManagerMock();
            var mockRecipientsService = new Mock<IRecipientsService>();
            var mockBloodbanksService = new Mock<IBloodBanksService>();
            var mockUsersService = new Mock<IUsersService>();
            mockUsersService.Setup(x => x.GetUserById<HospitalProfileInputModel>("123"))
                .Returns(new HospitalProfileInputModel()
                {
                    Id = "123",
                    Name = "Hospital1",
                });

            var controller = new HospitalsController(
                 mockHospitalsService.Object,
                 mockUserManagerService.Object,
                 mockRecipientsService.Object,
                 mockBloodbanksService.Object,
                 mockUsersService.Object);

            var result = controller.AddHospital();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AddHospitalHttpPostInvalidInputShouldThrowAnyExceptionTest()
        {
            var mockHospitalsService = new Mock<IHospitalsService>();
            var mockUserManagerService = this.GetUserManagerMock();
            var mockRecipientsService = new Mock<IRecipientsService>();
            var mockBloodbanksService = new Mock<IBloodBanksService>();
            var mockUsersService = new Mock<IUsersService>();
            var input = new HospitalProfileInputModel();

            var controller = new HospitalsController(
                  mockHospitalsService.Object,
                  mockUserManagerService.Object,
                  mockRecipientsService.Object,
                  mockBloodbanksService.Object,
                  mockUsersService.Object);

            Assert.ThrowsAnyAsync<Exception>(() => controller.AddHospital(input));
        }

        [Fact]
        public void AddHospitalHttpPostValidInputShouldRedirectResultTest()
        {
            var mockHospitalsService = new Mock<IHospitalsService>();
            var mockUserManagerService = this.GetUserManagerMock();
            var mockRecipientsService = new Mock<IRecipientsService>();
            var mockBloodbanksService = new Mock<IBloodBanksService>();
            var mockUsersService = new Mock<IUsersService>();

            var controller = new HospitalsController(
                  mockHospitalsService.Object,
                  mockUserManagerService.Object,
                  mockRecipientsService.Object,
                  mockBloodbanksService.Object,
                  mockUsersService.Object);

            var input = this.SeedData();

            var result = controller.AddHospital(input);
            Assert.IsType<RedirectToActionResult>(result.Result);
        }

        [Fact]
        public void AddHospitalHttpPostValidInputShouldRedirectToCorrectActionAndCotrollerTest()
        {
            var mockHospitalsService = new Mock<IHospitalsService>();
            var mockUserManagerService = this.GetUserManagerMock();
            var mockRecipientsService = new Mock<IRecipientsService>();
            var mockBloodbanksService = new Mock<IBloodBanksService>();
            var mockUsersService = new Mock<IUsersService>();

            var controller = new HospitalsController(
                  mockHospitalsService.Object,
                  mockUserManagerService.Object,
                  mockRecipientsService.Object,
                  mockBloodbanksService.Object,
                  mockUsersService.Object);

            var input = this.SeedData();

            var result = controller.AddHospital(input).Result;

            var redirect = result as RedirectToActionResult;
            Assert.Equal($"AllHospRecip", redirect.ActionName);
            Assert.Equal($"Recipients", redirect.ControllerName);
        }

        [Fact]
        public void AllHospitalsShouldReturnCorrectViewModelTest()
        {
            var mockHospitalsService = new Mock<IHospitalsService>();
            var mockUserManagerService = this.GetUserManagerMock();
            var mockRecipientsService = new Mock<IRecipientsService>();
            var mockBloodbanksService = new Mock<IBloodBanksService>();
            var mockUsersService = new Mock<IUsersService>();
            var viewModel = new AllHospitalsViewModel();

            var controller = new HospitalsController(
                  mockHospitalsService.Object,
                  mockUserManagerService.Object,
                  mockRecipientsService.Object,
                  mockBloodbanksService.Object,
                  mockUsersService.Object);

            var result = controller.AllHospitals(viewModel);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AllHospitalsHttpShouldBeEmptyTest()
        {
            var mockHospitalsService = new Mock<IHospitalsService>();
            var mockUserManagerService = this.GetUserManagerMock();
            var mockRecipientsService = new Mock<IRecipientsService>();
            var mockBloodbanksService = new Mock<IBloodBanksService>();
            var mockUsersService = new Mock<IUsersService>();
            var viewModel = new AllHospitalsViewModel();

            var controller = new HospitalsController(
                  mockHospitalsService.Object,
                  mockUserManagerService.Object,
                  mockRecipientsService.Object,
                  mockBloodbanksService.Object,
                  mockUsersService.Object);

            var result = controller.AllHospitals(viewModel);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsType<AllHospitalsViewModel>(viewResult.Model);

            viewModel = viewResult.Model as AllHospitalsViewModel;
            var actualResult = viewModel.Hospitals.Count();
            Assert.Equal(0, actualResult);
        }

        [Fact]
        public void AllHospitalsHttpShoulReturnNotNullTest()
        {
            var mockUsersService = new Mock<IUsersService>(); 
            var mockHospitalsService = new Mock<IHospitalsService>();
            mockHospitalsService
                .Setup(x => x.GetAllHospitals<HospitalInfoViewModel>(0, 0))
                .Returns(new List<HospitalInfoViewModel>()
                {
                    new HospitalInfoViewModel
                    {
                        Id = "123",
                        Name = "Hospital1",
                    },
                });
            var mockUserManagerService = this.GetUserManagerMock();
            var mockRecipientsService = new Mock<IRecipientsService>();
            var mockBloodbanksService = new Mock<IBloodBanksService>();

            var controller = new HospitalsController(
                  mockHospitalsService.Object,
                  mockUserManagerService.Object,
                  mockRecipientsService.Object,
                  mockBloodbanksService.Object,
                  mockUsersService.Object);

            Assert.NotNull(mockHospitalsService.Object);
        }

        [Fact]
        public void DetailsHospitalShouldReturnCorrectViewModelTest()
        {
            var mockHospitalsService = new Mock<IHospitalsService>();
            var mockUserManagerService = this.GetUserManagerMock();
            var mockRecipientsService = new Mock<IRecipientsService>();
            var mockBloodbanksService = new Mock<IBloodBanksService>();
            var mockUsersService = new Mock<IUsersService>();
            var viewModel = new AllHospitalBloodBagsViewModel();

            var controller = new HospitalsController(
                  mockHospitalsService.Object,
                  mockUserManagerService.Object,
                  mockRecipientsService.Object,
                  mockBloodbanksService.Object,
                  mockUsersService.Object);

            var result = controller.DetailsHospital("123", viewModel);

            Assert.IsType<ViewResult>(result);
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

        private HospitalProfileInputModel SeedData()
        {
            var input = new HospitalProfileInputModel
            {
                Id = "123",
                Name = "Hospital1",
                Phone = "123456789",
                Email = "Hospital1@hospital.com",
                Country = "Bulgaria",
                City = "Sofia",
                AdressDescription = "Sofia",
            };

            return input;
        }
    }
}
