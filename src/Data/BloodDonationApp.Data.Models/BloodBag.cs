namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class BloodBag : BaseDeletableModel<string>
    {
        public double Quantity { get; set; }

        public DateTime CollectionDate { get; set; }

        [ForeignKey(nameof(Donor))]
        public string DonorId { get; set; }

        public virtual DonorData Donor { get; set; }

        [ForeignKey(nameof(BloodType))]
        public string BloodTypeId { get; set; }

        public virtual BloodType BloodType { get; set; }
    }
}
