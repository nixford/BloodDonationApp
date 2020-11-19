namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class Request : BaseDeletableModel<string>
    {
        public Request()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(Hospital))]
        public string HospitalId { get; set; }

        public HospitalData Hospital { get; set; }

        public string HospitalName { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }

        public EmergencyStatus EmergencyStatus { get; set; }

        [ForeignKey(nameof(Recipient))]
        public string RecipientId { get; set; }

        public Recipient Recipient { get; set; }

        [ForeignKey(nameof(BloodType))]
        public string BloodTypeId { get; set; }

        public virtual BloodType BloodType { get; set; }

        public double NeededQuantity { get; set; }
    }
}
