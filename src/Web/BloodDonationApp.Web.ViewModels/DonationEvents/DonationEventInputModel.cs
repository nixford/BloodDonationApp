namespace BloodDonationApp.Web.ViewModels.DonationEvents
{
    using System;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Services.Mapping;

    public class DonationEventInputModel : IMapTo<DonationEvent>
    {
        public string HospitalId { get; set; }

        public double Quantity { get; set; }

        public DateTime CollectionDate { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public RhesusFactor RhesusFactor { get; set; }
    }
}
