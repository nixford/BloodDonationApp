namespace BloodDonationApp.Web.ViewModels.Request
{
    using System.Collections.Generic;

    public class AllRequestsViewModel
    {
        public IEnumerable<RequestInfoViewModel> Requests { get; set; }
    }
}
