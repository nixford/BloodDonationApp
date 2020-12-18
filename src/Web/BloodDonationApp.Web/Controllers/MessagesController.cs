namespace BloodDonationApp.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Common;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.Message;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class MessagesController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMessagesService messagesService;
        private const int take = 3;

        public MessagesController(
            UserManager<ApplicationUser> userManager,
            IMessagesService messagesService)
        {
            this.userManager = userManager;
            this.messagesService = messagesService;
        }

        [Authorize]
        public IActionResult CreateMessage()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(MessageInputViewModel inputView)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputView);
            }

            var authorId = this.userManager.GetUserId(this.User);
            var userName = this.userManager.GetUserName(this.User);

            await this.messagesService.Create(inputView.Content, authorId, userName, inputView.Email);

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AllMessages(AllMessagesViewModel viewModel, int page = 1)
        {
            viewModel.Messages = this.messagesService
                .GetAllMessages<MessageViewModel>(take, (page - 1) * take);

            var count = this.messagesService
                .GetAllMessages<MessageViewModel>()
                .Count();

            if (!string.IsNullOrEmpty(viewModel.SearchTerm))
            {
                viewModel.Messages = this.messagesService
                    .GetAllMessages<MessageViewModel>()
                    .Where(m => m.UserName.Contains(viewModel.SearchTerm)
                    || m.Email.Contains(viewModel.SearchTerm)
                    || m.Content.Contains(viewModel.SearchTerm));

                count = viewModel.Messages.Count();
            }

            viewModel.PagesCount = (int)Math.Ceiling((double)count / take);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;
            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [Route("Messages/Delete/{id:guid}")]
        public async Task<IActionResult> Delete(string id)
        {
            var message = this.messagesService
                .GetAllMessages<MessageViewModel>()
                .FirstOrDefault(m => m.Id == id);

            if (message == null)
            {
                return this.RedirectToAction("HttpStatusCodeHandler", "Error", this.NotFound());
            }

            await this.messagesService.Delete(id);

            return this.RedirectToAction("AllMessages", "Messages");
        }
    }
}
