namespace BloodDonationApp.Data.Models
{
    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class BloodType : BaseDeletableModel<string>
    {
        public BloodGroup BloodGroup { get; set; }

        public RhesusFactor RhesusFactor { get; set; }
    }
}
