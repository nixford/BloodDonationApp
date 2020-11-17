namespace BloodDonationApp.Web.Controllers
{
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.Request;
    using Microsoft.AspNetCore.Mvc;

    public class RequestsController : BaseController
    {
        private readonly IRequestsService requestsService;

        public RequestsController(IRequestsService requestsService)
        {
            this.requestsService = requestsService;
        }

        public IActionResult AllRequests(AllRequestsViewModel viewModel)
        {
            viewModel.Requests = this.requestsService.AllRequests<RequestInfoViewModel>();

            return this.View(viewModel);
        }
    }
}
