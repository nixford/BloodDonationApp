namespace BloodDonationApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BloodDonationApp.Data.Common.Models;

    public class DonationEvent : BaseModel<string>
    {
        public DonationEvent()
        {
            this.DonorsDonationEvents = new HashSet<DonorDonationEvent>();
        }

        public DateTime DateOfDonation { get; set; }

        public DonationRequest DonationRequest { get; set; }

        public virtual ICollection<DonorDonationEvent> DonorsDonationEvents { get; set; }
    }
}
