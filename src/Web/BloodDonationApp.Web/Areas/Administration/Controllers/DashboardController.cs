namespace BloodDonationApp.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using BloodDonationApp.Common;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.Administration.Users;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private const string RegisterAdminSeccessMessage = "Successfull registration of a new administrator: {0}!";
        private const string RegisterAdminErrorMessage = "The registration failed!";
        private const string RemoveSuccessMessage = "Successfully removed the {0} {1}!";
        private const string RemoveErrorMessage = "Missing {0} with email {1}!";

        private readonly IUsersService usersService;

        public DashboardController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            return this.View();
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
                // TODO: this.TempData["Success"] = string.Format(RemoveSuccessMessage, GlobalConstants.DonorRoleName, donorName);
            }
            catch
            {
                // TODO: this.TempData["Error"] = string.Format(RemoveSuccessMessage, GlobalConstants.DonorRoleName, inputModel.Email);
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
                // TODO: this.TempData["Success"] = string.Format(RemoveSuccessMessage, GlobalConstants.DonorRoleName, hospitalName);
            }
            catch
            {
                // TODO: this.TempData["Error"] = string.Format(RemoveSuccessMessage, GlobalConstants.DonorRoleName, inputModel.Email);
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
