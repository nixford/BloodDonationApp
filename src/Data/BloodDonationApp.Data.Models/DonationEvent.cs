namespace BloodDonationApp.Data.Models
{
    using System;

    using BloodDonationApp.Data.Common.Models;

    public class DonationEvent : BaseDeletableModel<string>
    {
        public DonationEvent()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime DateOfDonation { get; set; }

        public string? RequestId { get; set; }

        public Request Request { get; set; }

        public string UserDonorId { get; set; }

        public ApplicationUser UserDonor { get; set; }
    }
}
