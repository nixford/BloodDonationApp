namespace BloodDonationApp.Web.ViewModels.Request
{
    using System.Collections.Generic;

    using BloodDonationApp.Data.Models;

    public class AllRequestsViewModel
    {
        public AllRequestsViewModel()
        {
            this.Requests = new HashSet<RequestInfoViewModel>();
        }

        public IEnumerable<RequestInfoViewModel> Requests { get; set; }
    }
}
