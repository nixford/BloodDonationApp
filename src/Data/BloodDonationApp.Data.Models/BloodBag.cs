namespace BloodDonationApp.Data.Models
{
    using System;

    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class BloodBag : BaseDeletableModel<string>
    {
        public BloodGroup BloodGroup { get; set; }

        public RhesusFactor RhesusFactor { get; set; }

        public double Quantity { get; set; }

        public Donor Donor { get; set; }

        public DateTime CollectionDate { get; set; }

        public Examination Examination { get; set; }
    }
}
