namespace BloodDonationApp.Data.Models
{
    using System.Collections.Generic;

    using BloodDonationApp.Data.Common.Models;

    public class Location : BaseModel<string>
    {
        public Location()
        {
            this.HospitalsAdresses = new HashSet<Hospital>();
            this.DonorsLocations = new HashSet<Donor>();
        }

        public string Country { get; set; }

        public string City { get; set; }

        public string AdressDescription { get; set; }

        public virtual ICollection<Hospital> HospitalsAdresses { get; set; }

        public virtual ICollection<Donor> DonorsLocations { get; set; }
    }
}
