namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BloodDonationApp.Web.ViewModels.Request;

    public interface IRequestsService
    {
        Task<string> CreateRequestAsync(string hospitalId, RequestInputViewModel input);

        T GetById<T>(string id);

        IEnumerable<T> AllRequests<T>();

        Task DeleteAsync(string hospitalId);
    }
}
