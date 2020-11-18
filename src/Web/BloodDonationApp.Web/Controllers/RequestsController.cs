namespace BloodDonationApp.Web.Controllers
{
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.Request;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RequestsController : BaseController
    {
        private readonly IRequestsService requestsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRecipientsService recipientsService;
        // private string recipientId;

        public RequestsController(
            IRequestsService requestsService,
            UserManager<ApplicationUser> userManager,
            IRecipientsService recipientsService = null)
        {
            this.requestsService = requestsService;
            this.userManager = userManager;
            this.recipientsService = recipientsService;
        }

        [HttpGet]
        public IActionResult AddRequest(//string recipientId)
        {
            // this.recipientId = recipientId;
            return this.View();
        }

        // [Authorize(Roles = "Hospital")]
        [HttpPost]
        public async Task<IActionResult> AddRequest(RequestInputViewModel input)
        {
            //string recipientId = this.recipientId;

            var userId = this.userManager.GetUserId(this.User);

            var requestId = await this.requestsService.CreateRequestAsync(userId, input);

            await this.recipientsService.AddRequestForRecipient(requestId, recipientId);

            return this.RedirectToAction("AllRequests", "Requests");
        }

        public IActionResult AllRequests(AllRequestsViewModel viewModel)
        {
            viewModel.Requests = this.requestsService.AllRequests<RequestInfoViewModel>();

            return this.View(viewModel);
        }
    }
}
