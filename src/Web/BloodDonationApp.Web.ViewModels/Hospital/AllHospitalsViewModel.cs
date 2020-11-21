namespace BloodDonationApp.Web.ViewModels.Hospital
{
    using System.Collections.Generic;

    public class AllHospitalsViewModel
    {
        public int RecipientCount { get; set; }

        public IEnumerable<HospitalInfoViewModel> Hospitals { get; set; }
    }
}
