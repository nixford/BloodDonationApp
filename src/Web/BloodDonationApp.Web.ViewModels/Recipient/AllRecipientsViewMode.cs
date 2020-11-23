namespace BloodDonationApp.Web.ViewModels.Recipient
{
    using System.Collections.Generic;

    using BloodDonationApp.Services.Mapping;

    public class AllRecipientsViewMode
    {
        public IEnumerable<RecipientInfoViewModel> Recipients { get; set; }
    }
}
