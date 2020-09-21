namespace BloodDonationApp.Data.Models
{
    using System.Collections.Generic;

    public class Hospital
    {
        public Hospital()
        {
            this.Recipients = new HashSet<Recipient>();
        }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Location HospitalLocation { get; set; }

        public virtual BloodBank BloodBank { get; set; }

        public ICollection<Recipient> Recipients { get; set; }
    }
}
