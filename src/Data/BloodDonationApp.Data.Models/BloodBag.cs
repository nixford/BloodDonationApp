namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class BloodBag : BaseDeletableModel<string>
    {
        public BloodBag()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public double Quantity { get; set; }

        public DateTime CollectionDate { get; set; }

        [ForeignKey(nameof(BloodBank))]
        public string BloodBankId { get; set; }

        public BloodBank BloodBank { get; set; }

        [ForeignKey(nameof(DonorData))]
        public string DonorDataId { get; set; }

        public virtual DonorData DonorData { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public RhesusFactor RhesusFactor { get; set; }
    }
}
