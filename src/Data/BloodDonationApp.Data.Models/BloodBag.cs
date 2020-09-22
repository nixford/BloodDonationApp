namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class BloodBag : BaseModel<string>
    {
        public double Quantity { get; set; }

        public DateTime CollectionDate { get; set; }

        public virtual Donor Donor { get; set; }

        public virtual BloodType BloodType { get; set; }
    }
}
