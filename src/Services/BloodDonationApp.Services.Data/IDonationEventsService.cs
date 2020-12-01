namespace BloodDonationApp.Services.Data
{
    using System.Threading.Tasks;

    using BloodDonationApp.Web.ViewModels.DonationEvents;
    using BloodDonationApp.Web.ViewModels.Request;

    public interface IDonationEventsService
    {
        Task CreateDonation(string requestId, string recipientId, RequestInfoViewModel model, DonationEventInputModel viewModel);
    }
}
