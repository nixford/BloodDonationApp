namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BloodDonationApp.Web.ViewModels.Hospital;

    public interface IHospitalsService
    {
        Task CreateHospitalProfileAsync(HospitalProfileInputModel input, string userId);

        IEnumerable<T> GetAllHospitals<T>(int? take = null, int skip = 0);

        T GetHospitalDataById<T>(string userHospitalOrHospitalDataId);
    }
}
