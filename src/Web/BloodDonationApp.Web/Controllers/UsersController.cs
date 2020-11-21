namespace BloodDonationApp.Web.Controllers
{
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.Donor;
    using BloodDonationApp.Web.ViewModels.Hospital;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IUsersService usersService)
        {
            this.userManager = userManager;
            this.usersService = usersService;
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

            await this.usersService.CreateDonorProfileAsync(input, userId);

            return this.RedirectToAction("AllRequests", "Requests");
        }

        public IActionResult AllDonors(AllDonorsViewModel viewModel)
        {
            viewModel.Donors = this.usersService.GetAllDonors<DonorsInfoViewModel>();

            return this.View(viewModel);
        }
    }
}
