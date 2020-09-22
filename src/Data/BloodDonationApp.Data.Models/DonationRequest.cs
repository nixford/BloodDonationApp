namespace BloodDonationApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class DonationRequest : BaseDeletableModel<string>
    {
        public DonationRequest()
        {
            this.HospitaslDonationRequests = new HashSet<HospitalDonationRequest>();
            this.RecipientsDonationRequests = new HashSet<RecipientDonationRequest>();
        }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }

        public EmergencyStatus EmergencyStatus { get; set; }

        [ForeignKey(nameof(Contact))]
        public string ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        [ForeignKey(nameof(BloodType))]
        public string BloodTypeId { get; set; }

        public virtual BloodType BloodType { get; set; }

        public double NeededQuantity { get; set; }

        public virtual ICollection<HospitalDonationRequest> HospitaslDonationRequests { get; set; }

        public virtual ICollection<RecipientDonationRequest> RecipientsDonationRequests { get; set; }
    }
}
