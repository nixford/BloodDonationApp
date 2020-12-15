namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;

    public interface IRecipientsService
    {
        Task AddRecipientAsync(
            string userHospitalId,
            string firstName,
            string middleName,
            string lastName,
            int age,
            double neededQuantity,
            EmergencyStatus recipientEmergency,
            BloodGroup bloodGroup,
            RhesusFactor rhesusFactor);

        T GetById<T>(string recipientId);

        IEnumerable<T> AllHospitalRecipients<T>(string userHospitalId, int? take = null, int skip = 0);

        IEnumerable<T> TotalRecipients<T>(string userHospitalId);

        Task DeleteAsync(string hospitalId, string recipientId);

        Task AddRequestForRecipient(string requestId, string recipientId);
    }
}
