namespace BloodDonationApp.Web.ViewModels.Hospital
{
    using System.Collections.Generic;

    using BloodDonationApp.Web.ViewModels.Recipient;

    public class AllHospitalsViewModel
    {
        public IEnumerable<HospitalInfoViewModel> Hospitals { get; set; }
    }
}
