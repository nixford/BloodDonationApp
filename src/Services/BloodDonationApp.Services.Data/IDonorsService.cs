namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BloodDonationApp.Web.ViewModels.Donor;

    public interface IDonorsService
    {
        Task CreateDonorProfileAsync(DonorDataProfileInputModel input, string userId);

        IEnumerable<T> GetAllDonors<T>();
    }
}
