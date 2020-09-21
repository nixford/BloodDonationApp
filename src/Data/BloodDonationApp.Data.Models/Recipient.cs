namespace BloodDonationApp.Data.Models
{
    using System.Collections.Generic;

    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class Recipient : BaseDeletableModel<string>
    {
        public Recipient()
        {
            this.RecipientsDonationRequests = new HashSet<RecipientDonationRequest>();
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public double NeededQuantity { get; set; }

        public EmergencyStatus RecipientEmergency { get; set; }

        public virtual Contact PhoneAndEmailRecipient { get; set; }

        public virtual Hospital Hospital { get; set; }

        public virtual BloodType BloodType { get; set; }

        public virtual ICollection<RecipientDonationRequest> RecipientsDonationRequests { get; set; }
    }
}
