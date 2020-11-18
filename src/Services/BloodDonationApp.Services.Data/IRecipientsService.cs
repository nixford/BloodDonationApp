namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BloodDonationApp.Web.ViewModels.Recipient;

    public interface IRecipientsService
    {
        Task AddRecipientAsync(string userHospitalId, RecipientInputModel input);

        T GetById<T>(string recipientId);

        IEnumerable<T> AllHospitalRecipients<T>(string userHospitalId);

        IEnumerable<T> TotalRecipients<T>();

        Task DeleteAsync(string hospitalId, string recipientId);

        Task AddRequestForRecipient(string requestId, string recipientId);
    }
}
