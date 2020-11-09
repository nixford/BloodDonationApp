namespace BloodDonationApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class HospitalData : BaseDeletableModel<string>
    {
        public HospitalData()
        {
            this.Recipients = new HashSet<Recipient>();
            this.HospitalsDonationRequests = new HashSet<HospitalDonationRequest>();
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(Contact))]
        public string ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        [ForeignKey(nameof(Location))]
        public string LocationId { get; set; }

        public virtual Location Location { get; set; }

        [ForeignKey(nameof(BloodBank))]
        public string BloodBankId { get; set; }

        public virtual BloodBank BloodBank { get; set; }

        public virtual ICollection<Recipient> Recipients { get; set; }

        public virtual ICollection<HospitalDonationRequest> HospitalsDonationRequests { get; set; }
    }
}
