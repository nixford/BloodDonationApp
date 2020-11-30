namespace BloodDonationApp.Web.ViewModels.Recipient
{
    using System.Collections.Generic;

    using BloodDonationApp.Services.Mapping;

    public class AllRecipientsViewMode
    {
        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<RecipientInfoViewModel> Recipients { get; set; }
    }
}
