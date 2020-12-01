namespace BloodDonationApp.Web.ViewModels.Message
{
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;

    public class MessageViewModel : IMapTo<Message>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }
    }
}
