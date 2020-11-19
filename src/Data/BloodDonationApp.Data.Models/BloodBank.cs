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
            this.BloodBags = new HashSet<BloodBag>();
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(Hospital))]
        public string HospitalId { get; set; }

        public HospitalData Hospital { get; set; }

        public virtual ICollection<BloodBag> BloodBags { get; set; }
    }
}
