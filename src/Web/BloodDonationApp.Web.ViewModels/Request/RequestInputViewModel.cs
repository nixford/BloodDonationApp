namespace BloodDonationApp.Web.ViewModels.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using BloodDonationApp.Data.Models.Enums;

    public class RequestInputViewModel
    {
        public string RecipientId { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }

        public EmergencyStatus EmergencyStatus { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public RhesusFactor RhesusFactor { get; set; }

        public double NeededQuantity { get; set; }
    }
}
