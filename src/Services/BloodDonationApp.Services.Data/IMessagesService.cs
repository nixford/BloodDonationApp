namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessagesService
    {
        Task Create(string content, string authorId, string userName, string email);

        IEnumerable<T> GetAllMessages<T>(int? take = null, int skip = 0);

        Task<string> Delete(string id);
    }
}
