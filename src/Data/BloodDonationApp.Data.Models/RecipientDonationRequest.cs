namespace BloodDonationApp.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class RecipientDonationRequest : BaseDeletableModel<string>
    {
        [ForeignKey(nameof(Recipient))]
        public string RecipientId { get; set; }

        public Recipient Recipient { get; set; }

        [ForeignKey(nameof(DonationRequest))]
        public string DonationRequestId { get; set; }

        public DonationRequest DonationRequest { get; set; }
    }
}
