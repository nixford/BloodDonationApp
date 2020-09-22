namespace BloodDonationApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class Donor : BaseDeletableModel<string>
    {
        public Donor()
        {
            this.ExaminationsDonors = new HashSet<ExaminationDonor>();
            this.DonorsDonationEvents = new HashSet<DonorDonationEvent>();
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        [ForeignKey(nameof(Contact))]
        public string ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        [ForeignKey(nameof(Location))]
        public string LocationId { get; set; }

        public virtual Location Location { get; set; }

        public virtual EmergencyStatus DonorAveilableStatus { get; set; }

        [ForeignKey(nameof(BloodType))]
        public string BloodTypeId { get; set; }

        public virtual BloodType BloodType { get; set; }

        public virtual ICollection<ExaminationDonor> ExaminationsDonors { get; set; }

        public virtual ICollection<DonorDonationEvent> DonorsDonationEvents { get; set; }
    }
}
