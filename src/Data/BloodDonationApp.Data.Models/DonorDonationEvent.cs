namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class DonorDonationEvent : BaseDeletableModel<string>
    {
        public DonorDonationEvent()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(DonorData))]
        public string DonorDataId { get; set; }

        public DonorData DonorData { get; set; }

        [ForeignKey(nameof(DonationEvent))]
        public string DonationEventId { get; set; }

        public DonationEvent DonationEvent { get; set; }
    }
}
