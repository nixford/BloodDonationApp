namespace BloodDonationApp.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class Recipient : BaseDeletableModel<string>
    {
        public Recipient()
        {
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public double NeededQuantity { get; set; }

        public EmergencyStatus RecipientEmergency { get; set; }

        [ForeignKey(nameof(HospitalData))]
        public string HospitalId { get; set; }

        [ForeignKey(nameof(BloodType))]
        public string BloodTypeId { get; set; }

        public virtual BloodType BloodType { get; set; }
    }
}
