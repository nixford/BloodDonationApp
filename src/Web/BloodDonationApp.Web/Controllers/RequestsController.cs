namespace BloodDonationApp.Web.Controllers
{
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.Request;
    using Microsoft.AspNetCore.Mvc;

    public class RequestsController : BaseController
    {
        private readonly IUsersService usersService;

        public RequestsController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult AllRequests(AllRequestsViewModel viewModel)
        {
            viewModel.Requests = this.usersService.GetAllDonors<RequestInfoViewModel>();

            return this.View(viewModel);
        }
    }
}
