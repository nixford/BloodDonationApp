namespace BloodDonationApp.Data.Models
{
    using BloodDonationApp.Data.Common.Models;

    public class ExaminationDonor : BaseModel<string>
    {
        public string DonorId { get; set; }

        public Donor Donor { get; set; }

        public string ExaminationId { get; set; }

        public Examination Examination { get; set; }
    }
}
