namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class HospitalBloodBank : BaseDeletableModel<string>
    {
        public HospitalBloodBank()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(Hospital))]
        public string HospitalId { get; set; }

        public HospitalData Hospital { get; set; }

        [ForeignKey(nameof(BloodBank))]
        public string BloodBankId { get; set; }

        public BloodBank BloodBank { get; set; }
    }
}
