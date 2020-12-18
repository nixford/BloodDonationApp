namespace BloodDonationApp.Web.ViewModels.DonationEvents
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Services.Mapping;

    public class DonationEventInputModel : IMapTo<DonationEvent>
    {
        public string HospitalId { get; set; }

        [Required]
        [Range(0, 10000)]
        public double Quantity { get; set; }

        public DateTime CollectionDate => DateTime.UtcNow;

        [Required]
        [EnumDataType(typeof(BloodGroup))]
        public BloodGroup BloodGroup { get; set; }

        [Required]
        [EnumDataType(typeof(RhesusFactor))]
        public RhesusFactor RhesusFactor { get; set; }
    }
}
