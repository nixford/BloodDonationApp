namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class DonationRequest : BaseDeletableModel<string>
    {
        public DonationRequest()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public HospitalData Hospital { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }

        public EmergencyStatus EmergencyStatus { get; set; }

        [ForeignKey(nameof(BloodType))]
        public string BloodTypeId { get; set; }

        public virtual BloodType BloodType { get; set; }

        public double NeededQuantity { get; set; }
    }
}
