namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;

    public interface IBloodBanksService
    {
        IEnumerable<BloodBag> GetHospitalBloodBagsById(string id);
    }
}
