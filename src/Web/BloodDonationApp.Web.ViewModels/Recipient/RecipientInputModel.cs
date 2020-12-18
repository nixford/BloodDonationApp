namespace BloodDonationApp.Web.ViewModels.Recipient
{
    using System.ComponentModel.DataAnnotations;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class RecipientInputModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Range(1, 130)]
        public int Age { get; set; }

        [Required]
        [Range(0, 10000)]
        public double NeededQuantity { get; set; }

        [Required]
        [EnumDataType(typeof(EmergencyStatus))]
        public EmergencyStatus RecipientEmergency { get; set; }

        [Required]
        [EnumDataType(typeof(BloodGroup))]
        public BloodGroup BloodGroup { get; set; }

        [Required]
        [EnumDataType(typeof(RhesusFactor))]
        public RhesusFactor RhesusFactor { get; set; }
    }
}
