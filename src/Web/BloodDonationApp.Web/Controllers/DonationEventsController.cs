namespace BloodDonationApp.Web.Controllers
{
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DonationEventsController : BaseController
    {
        private readonly IDonationEventsService donationEventsService;
        private readonly UserManager<ApplicationUser> userManager;

        public DonationEventsController(
            IDonationEventsService donationEventsService, UserManager<ApplicationUser> userManager)
        {
            this.donationEventsService = donationEventsService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Donor")]
        public IActionResult Create(string requestOrHospitalId)
        {
            this.TempData.Remove("rhId");

            this.TempData.Add("rhId", requestOrHospitalId);

            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Donor")]
        public async Task<IActionResult> Create()
        {
            var requestOrHospitalId = this.TempData["rhId"].ToString();

            var userDonorId = this.userManager.GetUserId(this.User);

            await this.donationEventsService.CreateDonation(requestOrHospitalId, userDonorId);

            return this.RedirectToAction("AllRequests", "Requests");
        }
    }
}
