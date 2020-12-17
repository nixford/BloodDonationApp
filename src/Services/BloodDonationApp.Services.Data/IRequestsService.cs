namespace BloodDonationApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Web.ViewModels.Request;

    public interface IRequestsService
    {
        Task<string> CreateRequestAsync(
            string userId,
            string content,
            DateTime publishedOn,
            EmergencyStatus emergencyStatus,
            BloodGroup bloodGroup,
            RhesusFactor rhesusFactor,
            double neededQuantity);

        T GetById<T>(string id);

        IEnumerable<T> AllRequests<T>(string? userId, int? take = null, int skip = 0);

        Task DeleteAsync(string hospitalId);
    }
}
