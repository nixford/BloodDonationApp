namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BloodDonationApp.Web.ViewModels.Hospital;

    public interface IHospitalsService
    {
        Task CreateHospitalProfileAsync(HospitalProfileInputModel input, string userId);

        IEnumerable<T> GetAllHospitals<T>();

        T GetHospitalDataById<T>(string id);

        T GetHospitalDataByName<T>(string hospitalName);
    }
}
