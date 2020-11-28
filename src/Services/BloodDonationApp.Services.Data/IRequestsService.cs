namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Web.ViewModels.Request;

    public interface IRequestsService
    {
        Task<string> CreateRequestAsync(string hospitalId, RequestInputViewModel input);

        T GetById<T>(string id);

        IEnumerable<T> AllRequests<T>(int? take = null, int skip = 0);

        IEnumerable<Request> AllRequestsCount();

        Task DeleteAsync(string hospitalId);
    }
}
