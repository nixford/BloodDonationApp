﻿namespace BloodDonationApp.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class HospitalDonationRequest : BaseDeletableModel<string>
    {
        [ForeignKey(nameof(HospitalData))]
        public string HospitalDataId { get; set; }

        public HospitalData HospitalData { get; set; }

        [ForeignKey(nameof(DonationRequest))]
        public string DonationRequestId { get; set; }

        public DonationRequest DonationRequest { get; set; }
    }
}
