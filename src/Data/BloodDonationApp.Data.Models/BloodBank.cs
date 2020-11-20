namespace BloodDonationApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using BloodDonationApp.Data.Common.Models;

    public class BloodBank : BaseDeletableModel<string>
    {
        public BloodBank()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(HospitalData))]
        public string HospitalDataId { get; set; }

        public HospitalData HospitalData { get; set; }

        [ForeignKey(nameof(BloodBag))]
        public string BloodBagId { get; set; }

        public BloodBag BloodBag { get; set; }
    }
}
