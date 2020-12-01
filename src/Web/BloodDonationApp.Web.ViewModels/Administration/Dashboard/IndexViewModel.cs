namespace BloodDonationApp.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    using BloodDonationApp.Data.Models;

    public class IndexViewModel
    {
        public int SettingsCount { get; set; }

        public int DonorsCount { get; set; }

        public int HospitalsCount { get; set; }

        public int AdminsCount { get; set; }

        public string SearchTerm { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<ApplicationUser> DeletedUsers { get; set; }
    }
}
