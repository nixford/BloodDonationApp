namespace BloodDonationApp.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.Hospital;
    using BloodDonationApp.Web.ViewModels.Recipient;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HospitalsController : BaseController
    {
        private readonly IHospitalsService hospitalsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRecipientsService recipientsService;

        public HospitalsController(
            IHospitalsService hospitalsService,
            UserManager<ApplicationUser> userManager, 
            IRecipientsService recipientsService)
        {
            this.hospitalsService = hospitalsService;
            this.userManager = userManager;
            this.recipientsService = recipientsService;
        }

        [Authorize]
        public IActionResult AddHospital()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = this.hospitalsService.GetHospitalDataById<HospitalProfileInputModel>(userId);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddHospital(HospitalProfileInputModel inputModel)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.hospitalsService.CreateHospitalProfileAsync(inputModel, userId);

            return this.RedirectToAction("AllRequests", "Requests");
        }

        public IActionResult AllHospitals(AllHospitalsViewModel viewModel)
        {
            var userHospitalId = this.userManager.GetUserId(this.User);

            viewModel.Hospitals = this.hospitalsService.GetAllHospitals<HospitalInfoViewModel>();
            viewModel.RecipientCount =
                    this.recipientsService
                    .AllHospitalRecipients<AllRecipientsViewMode>(userHospitalId)
                    .Count();

            return this.View(viewModel);
        }

        public IActionResult DetailsHospital(string userHospitalId)
        {
            var viewModel = this.hospitalsService.GetHospitalDataById<HospitalInfoViewModel>(userHospitalId);

            return this.View(viewModel);
        }
    }
}
