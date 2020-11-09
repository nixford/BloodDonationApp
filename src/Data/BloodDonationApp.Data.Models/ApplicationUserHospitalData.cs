namespace BloodDonationApp.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class ApplicationUserHospitalData : BaseDeletableModel<string>
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(HospitalData))]
        public string HospitalDataId { get; set; }

        public HospitalData HospitalData { get; set; }
    }
}
