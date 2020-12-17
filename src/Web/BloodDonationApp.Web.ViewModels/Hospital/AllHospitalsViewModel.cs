namespace BloodDonationApp.Web.ViewModels.Hospital
{
    using System;
    using System.Collections.Generic;

    using BloodDonationApp.Web.ViewModels.Recipient;

    public class AllHospitalsViewModel
    {
        public Guid Id { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<HospitalInfoViewModel> Hospitals { get; set; }
    }
}
