namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class DonationEvent : BaseDeletableModel<string>
    {
        public DonationEvent()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime DateOfDonation { get; set; }

        [ForeignKey(nameof(Request))]
        public string RequestId { get; set; }

        public Request Request { get; set; }

        [ForeignKey(nameof(Donor))]
        public string DonorId { get; set; }

        public ApplicationUser Donor { get; set; }
    }
}
