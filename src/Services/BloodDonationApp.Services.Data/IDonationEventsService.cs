namespace BloodDonationApp.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public interface IDonationEventsService
    {
        Task CreateDonation(string requestId, string recipientId);
    }
}
