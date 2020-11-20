namespace BloodDonationApp.Data.Models
{
    using System;

    using BloodDonationApp.Data.Common.Models;

    public class ApplicationUserHospitalData : BaseDeletableModel<string>
    {
        public ApplicationUserHospitalData()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string HospitalDataId { get; set; }

        public HospitalData HospitalData { get; set; }
    }
}
