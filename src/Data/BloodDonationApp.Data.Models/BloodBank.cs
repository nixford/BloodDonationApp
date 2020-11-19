namespace BloodDonationApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BloodDonationApp.Data.Common.Models;

    public class BloodBank : BaseDeletableModel<string>
    {
        public BloodBank()
        {
            this.BloodBags = new HashSet<BloodBag>();
            this.Id = Guid.NewGuid().ToString();
        }

        public virtual ICollection<BloodBag> BloodBags { get; set; }
    }
}
