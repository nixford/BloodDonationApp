namespace BloodDonationApp.Web.ViewModels.Message
{
    using System.Collections.Generic;

    public class AllMessagesViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<MessageViewModel> Messages { get; set; }
    }
}
