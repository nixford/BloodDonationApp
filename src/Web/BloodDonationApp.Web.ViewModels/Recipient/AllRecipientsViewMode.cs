namespace BloodDonationApp.Web.ViewModels.Recipient
{
    using System.Collections.Generic;

    public class AllRecipientsViewMode
    {
        public IEnumerable<RecipientInfoViewModel> Recipients { get; set; }
    }
}
