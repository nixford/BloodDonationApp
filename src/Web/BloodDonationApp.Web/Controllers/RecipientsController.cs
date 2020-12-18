namespace BloodDonationApp.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BloodDonationApp.Common;
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

        [Authorize(Roles = GlobalConstants.HospitaltRoleName)]
        public IActionResult AddRecipient()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.HospitaltRoleName)]
        public async Task<IActionResult> AddRecipient(RecipientInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var hospitalUserId = this.userManager.GetUserId(this.User);

            await this.recipientsService
                .AddRecipientAsync(
                hospitalUserId,
                input.FirstName,
                input.MiddleName,
                input.LastName,
                input.Age,
                input.NeededQuantity,
                input.RecipientEmergency,
                input.BloodGroup,
                input.RhesusFactor);

            return this.RedirectToAction("AllHospRecip", "Recipients");
        }

        [Authorize(Roles = GlobalConstants.HospitaltRoleName)]
        public IActionResult AllHospRecip(AllRecipientsViewMode viewModel, int page = 1)
        {
            var hospitalUserId = this.userManager.GetUserId(this.User);

            viewModel.Recipients =
                this.recipientsService
                .AllHospitalRecipients<RecipientInfoViewModel>(hospitalUserId, take, (int)(page - 1) * take);

            var count = this.recipientsService
                .TotalRecipients<RecipientInfoViewModel>(hospitalUserId)
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

        [Authorize(Roles = GlobalConstants.HospitaltRoleName)]
        [Route("Recipients/Delete/{recipientId:guid}")]
        public async Task<IActionResult> Delete(string recipientId)
        {
            var userHospitalId = this.userManager.GetUserId(this.User);

            // Checks if the user-hospital has the recipient, which id is set for delete (if not - error 404)
            var recipient = this.recipientsService
                .AllHospitalRecipients<RecipientInfoViewModel>(userHospitalId)
                .FirstOrDefault(r => r.Id == recipientId);
            if (recipient == null)
            {
                return this.RedirectToAction("HttpStatusCodeHandler", "Error", this.NotFound());
            }

            await this.recipientsService.DeleteAsync(userHospitalId, recipientId);

            return this.RedirectToAction("AllHospRecip", "Recipients");
        }

        [Authorize(Roles = GlobalConstants.HospitaltRoleName)]
        [Route("Recipients/DetailsRecipient/{recipientId:guid}")]
        public IActionResult DetailsRecipient(string recipientId)
        {
            // Checks if the user-hospital has the recipient, which id is set for delete (if not - error 404)
            var userHospitalId = this.userManager.GetUserId(this.User);
            var recipient = this.recipientsService
                .AllHospitalRecipients<RecipientInfoViewModel>(userHospitalId)
                .FirstOrDefault(r => r.Id == recipientId);
            if (recipient == null)
            {
                return this.RedirectToAction("HttpStatusCodeHandler", "Error", this.NotFound());
            }

            var viewModel = this.recipientsService.GetById<RecipientInfoViewModel>(recipientId);

            return this.View(viewModel);
        }
    }
}
