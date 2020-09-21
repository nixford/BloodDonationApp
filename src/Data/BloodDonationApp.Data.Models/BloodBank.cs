﻿namespace BloodDonationApp.Data.Models
{
    using System.Collections.Generic;

    using BloodDonationApp.Data.Common.Models;

    public class BloodBank : BaseDeletableModel<string>
    {
        public BloodBank()
        {
            this.BloodBags = new HashSet<BloodBag>();
        }

        public virtual Hospital Hospital { get; set; }

        public ICollection<BloodBag> BloodBags { get; set; }
    }
}
