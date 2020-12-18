namespace BloodDonationApp.Web.Controllers
{
    using System.Threading.Tasks;
    using BloodDonationApp.Common;
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
            IDonorsService donorsService,
            IUsersService usersService)
        {
            this.userManager = userManager;
            this.donorsService = donorsService;
            this.usersService = usersService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.DonorRoleName)]
        public IActionResult AddDonor()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = this.usersService.GetUserById<DonorDataProfileInputModel>(userId);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.DonorRoleName)]
        public async Task<IActionResult> AddDonor(DonorDataProfileInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.donorsService.CreateDonorProfileAsync(input, userId);

            return this.RedirectToAction("AllRequests", "Requests");
        }
    }
}
