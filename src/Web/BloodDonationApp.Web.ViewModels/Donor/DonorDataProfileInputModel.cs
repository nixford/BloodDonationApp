namespace BloodDonationApp.Web.ViewModels.Donor
{
    using System;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Services.Mapping;

    public class DonorDataProfileInputModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public RhesusFactor RhesusFactor { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string AdressDescription { get; set; }

        public EmergencyStatus DonorAveilableStatus { get; set; }

        public DateTime ExaminationDate { get; set; }

        public string DiseaseName { get; set; }

        public string DiseaseDescription { get; set; }

        public DiseaseStatus DiseaseStatus { get; set; }
    }
}
