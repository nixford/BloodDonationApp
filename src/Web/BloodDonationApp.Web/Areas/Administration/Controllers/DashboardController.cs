namespace BloodDonationApp.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Common;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.Administration.Dashboard;
    using BloodDonationApp.Web.ViewModels.Administration.Users;
    using BloodDonationApp.Web.ViewModels.Donor;
    using BloodDonationApp.Web.ViewModels.Hospital;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private const string RegisterAdminSeccessMessage = "Successfull registration of a new administrator: {0}!";
        private const string RegisterAdminErrorMessage = "Error: The registration failed!";
        private const string RemoveSuccessMessage = "Successfully removed the {0} {1}!";
        private const string RemoveErrorMessage = "Error: Missing {0} with email {1}!";

        private readonly IUsersService usersService;
        private readonly IDonorsService donorsService;
        private readonly IHospitalsService hospitalsService;

        public DashboardController(
            IUsersService usersService,
            IDonorsService donorsService, 
            IHospitalsService hospitalsService)
        {
            this.usersService = usersService;
            this.donorsService = donorsService;
            this.hospitalsService = hospitalsService;
        }

        [HttpGet]
        public IActionResult Index(IndexViewModel viewModel)
        {
            viewModel.DonorsCount = this.donorsService
                .GetAllDonors<DonorsInfoViewModel>().Count();

            viewModel.HospitalsCount = this.hospitalsService
                .GetAllHospitals<HospitalInfoViewModel>().Count();

            viewModel.AdminsCount = this.usersService.GetAllUsers()
                .Count();

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult RemoveDonor()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveDonor(RemoveUserInputModel inputModel)
        {
            try
            {
                string donorName = await this.usersService.RemoveAdminAsync(inputModel.Email, GlobalConstants.DonorRoleName);
                this.TempData["Msg"] = string.Format(RemoveSuccessMessage, GlobalConstants.DonorRoleName, donorName);
            }
            catch
            {
                this.TempData["Msg"] = string.Format(RemoveErrorMessage, GlobalConstants.DonorRoleName, inputModel.Email);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public IActionResult RemoveHospital()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveHospital(RemoveUserInputModel inputModel)
        {
            try
            {
                string hospitalName = await this.usersService.RemoveAdminAsync(inputModel.Email, GlobalConstants.HospitaltRoleName);
                this.TempData["Msg"] = string.Format(RemoveSuccessMessage, GlobalConstants.HospitaltRoleName, hospitalName);
            }
            catch
            {
                this.TempData["Msg"] = string.Format(RemoveErrorMessage, GlobalConstants.HospitaltRoleName, inputModel.Email);
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
