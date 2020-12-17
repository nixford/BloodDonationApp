namespace BloodDonationApp.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.Request;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RequestsController : BaseController
    {
        private readonly IRequestsService requestsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRecipientsService recipientsService;
        private const int take = 5;

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
        public IActionResult AddRequest()
        {
            return this.View();
        }

        // [Authorize(Roles = "Hospital")]
        [HttpPost]
        [Route("Requests/AddRequest/{recipientId:guid}")]
        public async Task<IActionResult> AddRequest(string recipientId, RequestInputViewModel input)
        {
            var userId = this.userManager.GetUserId(this.User);

            var requestId = await this.requestsService.CreateRequestAsync(
                userId,
                input.Content,
                input.PublishedOn,
                input.EmergencyStatus,
                input.BloodGroup,
                input.RhesusFactor,
                input.NeededQuantity);

            await this.recipientsService.AddRequestForRecipient(requestId, recipientId);

            return this.RedirectToAction("AllRequests", "Requests");
        }

        public IActionResult AllRequests(AllRequestsViewModel viewModel, int page = 1)
        {
            viewModel.Requests = this.requestsService.AllRequests<RequestInfoViewModel>(take, (int)(page - 1) * take);

            var count = this.requestsService.AllRequests<RequestInfoViewModel>().Count();

            if (!string.IsNullOrEmpty(viewModel.SearchTerm))
            {
                viewModel.Requests = this.requestsService
                    .AllRequests<RequestInfoViewModel>(take, (int)(page - 1) * take)
                    .Where(r => r.EmergencyStatus.ToString().Contains(viewModel.SearchTerm)
                    || r.BloodGroup.ToString().Contains(viewModel.SearchTerm)
                    || r.RhesusFactor.ToString().Contains(viewModel.SearchTerm)
                    || r.HospitalName.Contains(viewModel.SearchTerm)
                    || r.Location.Country.Contains(viewModel.SearchTerm)
                    || r.Location.City.Contains(viewModel.SearchTerm)
                    || r.Location.AdressDescription.Contains(viewModel.SearchTerm));

                count = viewModel.Requests.Count();
            }

            viewModel.PagesCount = (int)Math.Ceiling((double)count / take);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        [Authorize(Roles = "Hospital")]
        [Route("Requests/Delete/{requestId:guid}")]
        public async Task<IActionResult> Delete(string requestId)
        {
            // var hospitalId = this.userManager.GetUserId(this.User);

            await this.requestsService.DeleteAsync(requestId);

            return this.RedirectToAction("AllRequests", "Requests");
        }
    }
}
