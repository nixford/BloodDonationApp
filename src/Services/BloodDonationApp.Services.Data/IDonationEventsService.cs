namespace BloodDonationApp.Services.Data
{
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models.Enums;

    public interface IDonationEventsService
    {
        Task CreateDonation(string requestId, string recipientId, double neededQuantity, double quantity, BloodGroup bloodGroup, RhesusFactor rhesusFactor);
    }
}
