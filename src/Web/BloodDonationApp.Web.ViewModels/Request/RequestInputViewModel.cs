namespace BloodDonationApp.Web.ViewModels.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using BloodDonationApp.Data.Models.Enums;

    public class RequestInputViewModel
    {
        public string RecipientId { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Content { get; set; }

        public DateTime PublishedOn => DateTime.UtcNow;

        [Required]
        [EnumDataType(typeof(EmergencyStatus))]
        public EmergencyStatus EmergencyStatus { get; set; }

        [Required]
        [EnumDataType(typeof(BloodGroup))]
        public BloodGroup BloodGroup { get; set; }

        [Required]
        [EnumDataType(typeof(RhesusFactor))]
        public RhesusFactor RhesusFactor { get; set; }

        [Required]
        [Range(0, 10000)]
        public double NeededQuantity { get; set; }
    }
}
