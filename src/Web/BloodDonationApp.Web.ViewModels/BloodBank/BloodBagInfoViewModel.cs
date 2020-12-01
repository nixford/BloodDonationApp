namespace BloodDonationApp.Web.ViewModels.BloodBank
{
    using System;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Services.Mapping;

    public class BloodBagInfoViewModel : IMapTo<BloodBag>
    {
        public double QuantityCondition { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public RhesusFactor RhesusFactor { get; set; }

        public DateTime CollectionDate { get; set; }
    }
}
