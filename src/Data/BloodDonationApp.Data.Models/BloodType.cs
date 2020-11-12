namespace BloodDonationApp.Data.Models
{
    using System;

    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class BloodType : BaseDeletableModel<string>
    {
        public BloodType()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public BloodGroup BloodGroup { get; set; }

        public RhesusFactor RhesusFactor { get; set; }
    }
}
