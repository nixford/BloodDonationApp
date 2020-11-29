namespace BloodDonationApp.Web.Controllers
{
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.DonationEvents;
    using BloodDonationApp.Web.ViewModels.Request;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DonationEventsController : BaseController
    {
        private readonly IDonationEventsService donationEventsService;
        private readonly IRequestsService requestsService;
        private readonly UserManager<ApplicationUser> userManager;

        public DonationEventsController(
            IDonationEventsService donationEventsService,
            UserManager<ApplicationUser> userManager,
            IRequestsService requestsService)
        {
            this.donationEventsService = donationEventsService;
            this.userManager = userManager;
            this.requestsService = requestsService;
        }

        [HttpGet]
        [Authorize(Roles = "Donor")]
        public IActionResult Create(string requestId, RequestInfoViewModel viewModel)
        {
            viewModel = this.requestsService.GetById<RequestInfoViewModel>(requestId);

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Donor")]
        public IActionResult CreateWORequest(string hospitalId, DonationEventInputModel viewModel)
        {
            viewModel.HospitalId = hospitalId;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Donor")]
        public async Task<IActionResult> Create(string userId, DonationEventInputModel viewModel)
        {
            var userDonorId = this.userManager.GetUserId(this.User);

            var id = userId != null ? userId : viewModel.HospitalId;

            await this.donationEventsService.CreateDonation(id, userDonorId, viewModel);

            return this.RedirectToAction("QAndA", "Home");
        }
    }
}
