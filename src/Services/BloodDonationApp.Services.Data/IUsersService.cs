namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BloodDonationApp.Web.ViewModels.Donor;
    using BloodDonationApp.Web.ViewModels.Hospital;

    public interface IUsersService
    {
        T GetUserById<T>(string id);

        T GetUserByName<T>(string userName);

        Task CreateDonorProfileAsync(DonorDataProfileInputModel input, string userId);

        IEnumerable<T> GetAllDonors<T>();

        IEnumerable<T> GetTopDonors<T>();

        string GetUserEmailById(string userId);

        Task<bool> AddAdmin(string userName, string email, string password);

        Task<string> RemoveAdminAsync(string email, string role);

        Task<string> RemoveUserAsync(string email);
    }
}
