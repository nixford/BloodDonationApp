namespace BloodDonationApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BloodDonationApp.Data.Common.Models;

    public class Location : BaseDeletableModel<string>
    {
        public Location()
        {
            this.HospitalsAdresses = new HashSet<HospitalData>();
            this.DonorsLocations = new HashSet<DonorData>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string Country { get; set; }

        public string City { get; set; }

        public string AdressDescription { get; set; }

        public virtual ICollection<HospitalData> HospitalsAdresses { get; set; }

        public virtual ICollection<DonorData> DonorsLocations { get; set; }
    }
}
