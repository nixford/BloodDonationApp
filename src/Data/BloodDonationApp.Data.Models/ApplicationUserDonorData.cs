namespace BloodDonationApp.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class ApplicationUserDonorData : BaseDeletableModel<string>
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(DonorData))]
        public string DonorDataId { get; set; }

        public DonorData DonorData { get; set; }
    }
}
