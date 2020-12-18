namespace BloodDonationApp.Web.ViewModels.Hospital
{
    using System.ComponentModel.DataAnnotations;

    using BloodDonationApp.Data.Models.Enums;

    public class EmptyInputViewModel
    {
        [Required]
        [EnumDataType(typeof(BloodGroup))]
        public BloodGroup BloodGroup { get; set; }

        [Required]
        [EnumDataType(typeof(RhesusFactor))]
        public RhesusFactor RhesusFactor { get; set; }
    }
}
