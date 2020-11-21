namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;

    using BloodDonationApp.Data.Models;

    public interface IBloodBanksService
    {
        IEnumerable<BloodBag> GetHospitalBloodBagsById(string userHospitalId);
    }
}
