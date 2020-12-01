namespace BloodDonationApp.Web.ViewModels.Recipient
{
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Services.Mapping;

    public class RecipientInfoViewModel : IMapTo<Recipient>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public double NeededQuantity { get; set; }

        public EmergencyStatus RecipientEmergency { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public RhesusFactor RhesusFactor { get; set; }
    }
}
