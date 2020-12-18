namespace BloodDonationApp.Web.Controllers
{
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
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
        [Route("DonationEvents/Create/{requestId:guid}")]
        public IActionResult Create(string requestId, RequestInfoViewModel viewModel)
        {
            viewModel = this.requestsService.GetById<RequestInfoViewModel>(requestId);

            if (viewModel == null)
            {
                return this.RedirectToAction("HttpStatusCodeHandler", "Error", this.NotFound());
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Donor")]
        [Route("DonationEvents/CreateWORequest/{id:guid}")]
        public IActionResult CreateWORequest(string id, DonationEventInputModel viewModel)
        {
            viewModel.HospitalId = id;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Donor")]
        public async Task<IActionResult> Create(DonationEventInputModel viewModel, RequestInfoViewModel model)
        {
            var userDonorId = this.userManager.GetUserId(this.User);

            var hospitalDataId = model.HospitalDataId != null ? model.HospitalDataId : viewModel.HospitalId;

            if (hospitalDataId == null)
            {
                return this.RedirectToAction("HttpStatusCodeHandler", "Error", this.NotFound());
            }

            double neededQuantity = model.NeededQuantity;
            double quantity = viewModel.Quantity;
            BloodGroup bloodGroup = viewModel.BloodGroup;
            RhesusFactor rhesusFactor = viewModel.RhesusFactor;

            await this.donationEventsService
                .CreateDonation(
                    hospitalDataId,
                    userDonorId,
                    neededQuantity,
                    quantity,
                    bloodGroup,
                    rhesusFactor);

            return this.RedirectToAction("QAndA", "Home");
        }
    }
}
