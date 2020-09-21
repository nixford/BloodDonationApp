namespace BloodDonationApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class Donor : BaseDeletableModel<string>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Location DonorLocation { get; set; }

        public EmergencyStatus DonorAveilableStatus { get; set; }

        [Required]
        [ForeignKey(nameof(BloodType))]
        public string BloodTypeId { get; set; }

        public BloodType BloodType { get; set; }
    }
}
