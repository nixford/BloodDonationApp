namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class HospitalDataBloodBank : BaseDeletableModel<string>
    {
        public HospitalDataBloodBank()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string HospitalId { get; set; }

        public HospitalData Hospital { get; set; }

        public string BloodBankId { get; set; }

        public BloodBank BloodBank { get; set; }
    }
}
