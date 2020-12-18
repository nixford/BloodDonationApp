namespace BloodDonationApp.Services.Data.Tests.ControllersTest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Web.Areas.Administration.Controllers;
    using BloodDonationApp.Web.ViewModels.Administration.Dashboard;
    using BloodDonationApp.Web.ViewModels.Donor;
    using BloodDonationApp.Web.ViewModels.Hospital;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class DashboardControllerTest
    {
        [Fact]
        public void IndexShouldReturnCorrectDataInViewModelTest()
        {
            var mockUsersService = new Mock<IUsersService>();
            mockUsersService.Setup(x => x.GetAllAdmins()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    UserName = "Admin1",
                    Email = "Admin1@gmail.com",
                },
            });

            var mockDonorsService = new Mock<IDonorsService>();
            mockDonorsService.Setup(x => x.GetAllDonors<DonorsInfoViewModel>()).Returns(new List<DonorsInfoViewModel>()
            {
                new DonorsInfoViewModel
                {
                    FirstName = "Donor1",
                    LastName = "Donor1",
                },
            });

            var mockHospitalsService = new Mock<IHospitalsService>();
            mockHospitalsService.Setup(x => x.GetAllHospitals<HospitalInfoViewModel>(null, 0)).Returns(new List<HospitalInfoViewModel>()
            {
                new HospitalInfoViewModel
                {
                    Name = "Hospital1",
                    RecipientCount = 5,
                },
            });

            var controller = new DashboardController(
                mockUsersService.Object,
                mockDonorsService.Object,
                mockHospitalsService.Object);

            var viewModel = new IndexViewModel();

            var result = controller.Index(viewModel);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsType<IndexViewModel>(viewResult.Model);

            viewModel = viewResult.Model as IndexViewModel;
            Assert.Equal(1, viewModel.AdminsCount);
            Assert.Equal(1, viewModel.DonorsCount);
            Assert.Equal(1, viewModel.HospitalsCount);
        }

        [Fact]
        public void IndexShouldReturnCorrectViewModelTest()
        {
            var mockUsersService = new Mock<IUsersService>();
            var mockDonorsService = new Mock<IDonorsService>();
            var mockHospitalsService = new Mock<IHospitalsService>();
            var viewModel = new IndexViewModel();

            var controller = new DashboardController(
                mockUsersService.Object,
                mockDonorsService.Object,
                mockHospitalsService.Object);

            var result = controller.Index(viewModel);

            Assert.IsType<ViewResult>(result);
        }
    }
}
