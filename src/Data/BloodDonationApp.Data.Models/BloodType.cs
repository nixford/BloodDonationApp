﻿namespace BloodDonationApp.Data.Models
{
    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class BloodType : BaseModel<string>
    {
        public BloodGroup BloodGroup { get; set; }

        public BloodRhesusFactor RhesusFactor { get; set; }
    }
}
