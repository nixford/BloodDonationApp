namespace BloodDonationApp.Web.Controllers
{
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.Donor;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DonorsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDonorsService donorsService;
        private readonly IUsersService usersService;

        public DonorsController(
            UserManager<ApplicationUser> userManager,
            IDonorsService donorsService)
        {
            this.userManager = userManager;
            this.donorsService = donorsService;
        }

        [Authorize]
        public IActionResult AddDonor()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = this.usersService.GetUserById<DonorDataProfileInputModel>(userId);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddDonor(DonorDataProfileInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.donorsService.CreateDonorProfileAsync(input, userId);

            return this.RedirectToAction("AllRequests", "Requests");
        }

        public IActionResult AllDonors(AllDonorsViewModel viewModel)
        {
            viewModel.Donors = this.donorsService.GetAllDonors<DonorsInfoViewModel>();

            return this.View(viewModel);
        }
    }
}
