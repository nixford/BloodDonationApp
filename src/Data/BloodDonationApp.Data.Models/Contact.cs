namespace BloodDonationApp.Data.Models
{
    using System;

    using BloodDonationApp.Data.Common.Models;

    public class Contact : BaseDeletableModel<string>
    {
        public Contact()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
