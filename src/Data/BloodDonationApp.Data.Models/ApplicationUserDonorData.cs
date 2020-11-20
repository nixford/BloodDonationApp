namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class ApplicationUserDonorData : BaseDeletableModel<string>
    {
        public ApplicationUserDonorData()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string DonorDataId { get; set; }

        public DonorData DonorData { get; set; }
    }
}
