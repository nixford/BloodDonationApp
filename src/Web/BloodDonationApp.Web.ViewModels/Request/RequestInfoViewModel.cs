namespace BloodDonationApp.Web.ViewModels.Request
{
    using System;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Services.Mapping;

    public class RequestInfoViewModel : IMapTo<Request>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string HospitalName { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }

        public string EmergencyStatus { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public RhesusFactor RhesusFactor { get; set; }

        public double NeededQuantity { get; set; }

        public Location Location { get; set; }
    }
}
