namespace BloodDonationApp.Web.ViewModels.Hospital
{
    using System.Collections.Generic;

    using BloodDonationApp.Web.ViewModels.Recipient;

    public class AllHospitalsViewModel
    {

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<HospitalInfoViewModel> Hospitals { get; set; }
    }
}
