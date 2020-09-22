namespace BloodDonationApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class BloodBank : BaseModel<string>
    {
        public BloodBank()
        {
            this.BloodBags = new HashSet<BloodBag>();
        }

        [ForeignKey(nameof(Hospital))]
        public string HospitalId { get; set; }

        public virtual Hospital Hospital { get; set; }

        public virtual ICollection<BloodBag> BloodBags { get; set; }
    }
}
