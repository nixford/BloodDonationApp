namespace BloodDonationApp.Web.ViewModels.Donor
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Services.Mapping;

    public class DonorDataProfileInputModel : IMapTo<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Range(18, 65, ErrorMessage = "Donor can be human in age between 18 and 65")]
        public int Age { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EnumDataType(typeof(BloodGroup))]
        public BloodGroup BloodGroup { get; set; }

        [Required]
        [EnumDataType(typeof(RhesusFactor))]
        public RhesusFactor RhesusFactor { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Country { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string AdressDescription { get; set; }

        [Required]
        [EnumDataType(typeof(EmergencyStatus))]
        public EmergencyStatus DonorAveilableStatus { get; set; }

        public DateTime ExaminationDate { get; set; }

        [Required]
        public string DiseaseName { get; set; }

        public string DiseaseDescription => this.DiseaseName;

        public DiseaseStatus DiseaseStatus
            => this.DiseaseName != "None"
            ? DiseaseStatus.Positive
            : DiseaseStatus.Negative;
    }
}
