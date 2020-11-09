namespace BloodDonationApp.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class HospitalBloodBank : BaseDeletableModel<string>
    {
        [ForeignKey(nameof(Hospital))]
        public string HospitalId { get; set; }

        public HospitalData Hospital { get; set; }

        [ForeignKey(nameof(BloodBank))]
        public string BloodBankId { get; set; }

        public BloodBank BloodBank { get; set; }
    }
}
