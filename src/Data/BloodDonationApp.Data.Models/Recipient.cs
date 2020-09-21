namespace BloodDonationApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class Recipient : BaseDeletableModel<string>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public double NeededQuantity { get; set; }

        public EmergencyStatus RecipientEmergency { get; set; }

        [Required]
        [ForeignKey(nameof(Hospital))]
        public Hospital HospitalId { get; set; }

        public Hospital Hospital { get; set; }

        [Required]
        [ForeignKey(nameof(BloodType))]
        public string BloodTypeId { get; set; }

        public BloodType BloodType { get; set; }
    }
}
