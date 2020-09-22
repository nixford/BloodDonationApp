namespace BloodDonationApp.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class ExaminationDonor : BaseDeletableModel<string>
    {
        [ForeignKey(nameof(Donor))]
        public string DonorId { get; set; }

        public Donor Donor { get; set; }

        [ForeignKey(nameof(Examination))]
        public string ExaminationId { get; set; }

        public Examination Examination { get; set; }
    }
}
