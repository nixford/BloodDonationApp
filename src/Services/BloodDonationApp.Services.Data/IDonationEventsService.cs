namespace BloodDonationApp.Services.Data
{
    using System.Threading.Tasks;

    using BloodDonationApp.Web.ViewModels.DonationEvents;

    public interface IDonationEventsService
    {
        Task CreateDonation(string requestId, string recipientId, DonationEventInputModel viewModel);
    }
}
