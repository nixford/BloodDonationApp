namespace BloodDonationApp.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class HospitalDonationRequest : BaseDeletableModel<string>
    {
        [ForeignKey(nameof(Hospital))]
        public string HospitalId { get; set; }

        public Hospital Hospital { get; set; }

        [ForeignKey(nameof(DonationRequest))]
        public string DonationRequestId { get; set; }

        public DonationRequest DonationRequest { get; set; }
    }
}
