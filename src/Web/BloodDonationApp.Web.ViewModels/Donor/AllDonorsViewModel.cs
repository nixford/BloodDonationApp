using System.Collections.Generic;

namespace BloodDonationApp.Web.ViewModels.Donor
{
    public class AllDonorsViewModel
    {
        public IEnumerable<DonorsInfoViewModel> Donors { get; set; }
    }
}
