namespace BloodDonationApp.Data.Models
{
    using BloodDonationApp.Data.Common.Models;

    public class RecipientDonationRequest : BaseModel<string>
    {
        public string RecipientId { get; set; }

        public Recipient Recipient { get; set; }

        public string DonationRequestId { get; set; }

        public DonationRequest DonationRequest { get; set; }
    }
}
