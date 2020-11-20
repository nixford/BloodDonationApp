namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class BloodBag : BaseDeletableModel<string>
    {
        public BloodBag()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public double Quantity { get; set; }

        public DateTime CollectionDate { get; set; }

        [ForeignKey(nameof(DonorData))]
        public string DonorDataId { get; set; }

        public virtual DonorData DonorData { get; set; }

        [ForeignKey(nameof(BloodType))]
        public string BloodTypeId { get; set; }

        public virtual BloodType BloodType { get; set; }
    }
}
