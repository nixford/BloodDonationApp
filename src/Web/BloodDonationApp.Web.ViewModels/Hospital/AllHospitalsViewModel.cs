namespace BloodDonationApp.Web.ViewModels.Hospital
{
    using System.Collections.Generic;

    public class AllHospitalsViewModel
    {
        public IEnumerable<HospitalInfoViewModel> Hospitals { get; set; }
    }
}
