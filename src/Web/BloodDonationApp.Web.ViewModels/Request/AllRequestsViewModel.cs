namespace BloodDonationApp.Web.ViewModels.Request
{
    using System.Collections.Generic;

    using BloodDonationApp.Data.Models;

    public class AllRequestsViewModel
    {
        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<RequestInfoViewModel> Requests { get; set; }
    }
}
