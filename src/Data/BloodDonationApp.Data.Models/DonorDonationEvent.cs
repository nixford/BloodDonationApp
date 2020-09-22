namespace BloodDonationApp.Data.Models
{
    using BloodDonationApp.Data.Common.Models;

    public class DonorDonationEvent : BaseModel<string>
    {
        public string DonorId { get; set; }

        public Donor Donor { get; set; }

        public string DonationEventId { get; set; }

        public DonationEvent DonationEvent { get; set; }
    }
}
