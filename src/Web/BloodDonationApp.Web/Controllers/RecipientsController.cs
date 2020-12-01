namespace BloodDonationApp.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.Recipient;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RecipientsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRecipientsService recipientsService;
        private readonly IRequestsService requestsService;
        private const int take = 5;

        public RecipientsController(
            UserManager<ApplicationUser> userManager,
            IRecipientsService recipientsService,
            IRequestsService requestsService)
        {
            this.userManager = userManager;
            this.recipientsService = recipientsService;
            this.requestsService = requestsService;
        }

        [HttpGet]
        public IActionResult AddRecipient()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRecipient(RecipientInputModel input)
        {
            var hospitalUserId = this.userManager.GetUserId(this.User);

            await this.recipientsService.AddRecipientAsync(hospitalUserId, input);

            return this.RedirectToAction("AllHospRecip", "Recipients");
        }

        public IActionResult AllHospRecip(AllRecipientsViewMode viewModel, int page = 1)
        {
            var hospitalUserId = this.userManager.GetUserId(this.User);

            viewModel.Recipients =
                this.recipientsService
                .AllHospitalRecipients<RecipientInfoViewModel>(hospitalUserId, take, (int)(page - 1) * take);

            var count = this.recipientsService
                .TotalRecipients(hospitalUserId)
                .Count();

            if (!string.IsNullOrEmpty(viewModel.SearchTerm))
            {
                viewModel.Recipients = this.recipientsService
                    .AllHospitalRecipients<RecipientInfoViewModel>(hospitalUserId)
                    .Where(r => r.FirstName.Contains(viewModel.SearchTerm)
                    || r.LastName.Contains(viewModel.SearchTerm)
                    || r.BloodGroup.ToString().Contains(viewModel.SearchTerm)
                    || r.RhesusFactor.ToString().Contains(viewModel.SearchTerm));

                count = viewModel.Recipients.Count();
            }

            viewModel.PagesCount = (int)Math.Ceiling((double)count / take);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(string recipientId)
        {
            var hospitalId = this.userManager.GetUserId(this.User);

            await this.recipientsService.DeleteAsync(hospitalId, recipientId);

            return this.RedirectToAction("AllHospRecip", "Recipients");
        }

        public IActionResult DetailsRecipient(string recipientId)
        {
            var viewModel = this.recipientsService.GetById<RecipientInfoViewModel>(recipientId);

            return this.View(viewModel);
        }
    }
}
