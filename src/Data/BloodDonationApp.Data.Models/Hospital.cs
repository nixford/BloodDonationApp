namespace BloodDonationApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class Hospital : BaseDeletableModel<string>
    {
        public Hospital()
        {
            this.Recipients = new HashSet<Recipient>();
            this.HospitalsDonationRequests = new HashSet<HospitalDonationRequest>();
        }

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
