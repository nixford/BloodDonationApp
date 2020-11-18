namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class RecipientHospitalData : BaseDeletableModel<string>
    {
        public RecipientHospitalData()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(HospitalData))]
        public string HospitalDataId { get; set; }

        public HospitalData HospitalData { get; set; }

        [ForeignKey(nameof(Recipient))]
        public string RecipientId { get; set; }

        public Recipient Recipient { get; set; }
    }
}
