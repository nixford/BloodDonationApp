namespace BloodDonationApp.Data.Models
{
    using BloodDonationApp.Data.Common.Models;

    public class HospitalDonationRequest : BaseModel<string>
    {
        public string HospitalId { get; set; }

        public Hospital Hospital { get; set; }

        public string DonationRequestId { get; set; }

        public DonationRequest DonationRequest { get; set; }
    }
}
