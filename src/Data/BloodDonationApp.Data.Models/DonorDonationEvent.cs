namespace BloodDonationApp.Data.Models
{
    public class DonorDonationEvent
    {
        public string DonorId { get; set; }

        public Donor Donor { get; set; }

        public string DonationEventId { get; set; }

        public DonationEvent DonationEvent { get; set; }
    }
}
