namespace BloodDonationApp.Web.ViewModels.Donor
{
    using System.Collections.Generic;

    public class AllDonorsViewModel
    {
        public IEnumerable<DonorsInfoViewModel> Donors { get; set; }
    }
}
