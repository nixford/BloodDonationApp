namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Web.ViewModels.Hospital;

    public interface IHospitalsService
    {
        Task CreateHospitalProfileAsync(HospitalProfileInputModel input, string userId);

        IEnumerable<T> GetAllHospitals<T>(int? take = null, int skip = 0);

        IEnumerable<HospitalData> GetAllHospitalsCount();

        T GetHospitalDataById<T>(string id);

        T GetHospitalDataByName<T>(string hospitalName);
    }
}
