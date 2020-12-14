namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models;

    public interface IUsersService
    {
        T GetUserById<T>(string id);

        IEnumerable<ApplicationUser> GetAllAdmins();

        IEnumerable<ApplicationUser> GetAllDeletedUsers(int? take = null, int skip = 0);

        T GetUserByName<T>(string userName);

        string GetUserEmailById(string userId);

        Task<bool> AddAdmin(string userName, string email, string password);

        Task<string> RemoveUserAsync(string email);
    }
}
