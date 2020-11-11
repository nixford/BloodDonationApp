namespace BloodDonationApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class DonationEvent : BaseDeletableModel<string>
    {
        public DonationEvent()
        {
            this.DonorsDonationEvents = new HashSet<DonorDonationEvent>();
        }

        public DateTime DateOfDonation { get; set; }

        [ForeignKey(nameof(DonationRequest))]
        public string DonationRequestId { get; set; }

        public Request DonationRequest { get; set; }

        public virtual ICollection<DonorDonationEvent> DonorsDonationEvents { get; set; }
    }
}
