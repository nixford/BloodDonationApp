namespace BloodDonationApp.Data.Models
{
    public class HospitalDonationRequest
    {
        public string HospitalId { get; set; }

        public Hospital Hospital { get; set; }

        public string DonationRequestId { get; set; }

        public DonationRequest DonationRequest { get; set; }
    }
}
