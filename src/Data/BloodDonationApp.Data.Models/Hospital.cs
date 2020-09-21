namespace BloodDonationApp.Data.Models
{
    using System.Collections.Generic;

    using BloodDonationApp.Data.Common.Models;

    public class Hospital : BaseDeletableModel<string>
    {
        public Hospital()
        {
            this.Recipients = new HashSet<Recipient>();
            this.HospitalsDonationRequests = new HashSet<HospitalDonationRequest>();
        }

        public string Name { get; set; }

        public virtual Contact PhoneAndEmailHospital { get; set; }

        public virtual Location HospitalLocation { get; set; }

        public virtual BloodBank BloodBank { get; set; }

        public virtual ICollection<Recipient> Recipients { get; set; }

        public virtual ICollection<HospitalDonationRequest> HospitalsDonationRequests { get; set; }
    }
}
