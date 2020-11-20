namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class RecipientRequest : BaseDeletableModel<string>
    {
        public RecipientRequest()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string RecipientId { get; set; }

        public Recipient Recipient { get; set; }

        public string RequestId { get; set; }

        public Request Request { get; set; }
    }
}
