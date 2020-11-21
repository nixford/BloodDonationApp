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
            this.BloodBags = new HashSet<BloodBag>();
        }

        [ForeignKey(nameof(HospitalData))]
        public string HospitalDataId { get; set; }

        public HospitalData HospitalData { get; set; }

        public virtual ICollection<BloodBag> BloodBags { get; set; }
    }
}
