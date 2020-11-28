namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Web.ViewModels.Recipient;

    public interface IRecipientsService
    {
        Task AddRecipientAsync(string userHospitalId, RecipientInputModel input);

        T GetById<T>(string recipientId);

        IEnumerable<T> AllHospitalRecipients<T>(string userHospitalId, int? take = null, int skip = 0);

        IEnumerable<Recipient> TotalRecipients(string userHospitalId);

        Task DeleteAsync(string hospitalId, string recipientId);

        Task AddRequestForRecipient(string requestId, string recipientId);
    }
}
