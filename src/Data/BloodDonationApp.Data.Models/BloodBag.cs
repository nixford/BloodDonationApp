namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class BloodBag : BaseDeletableModel<string>
    {
        public double Quantity { get; set; }

        public Donor Donor { get; set; }

        public DateTime CollectionDate { get; set; }

        public Examination Examination { get; set; }

        [Required]
        [ForeignKey(nameof(BloodType))]
        public string BloodTypeId { get; set; }

        public BloodType BloodType { get; set; }
    }
}
