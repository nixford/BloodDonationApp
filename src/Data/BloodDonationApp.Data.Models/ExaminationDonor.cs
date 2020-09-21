namespace BloodDonationApp.Data.Models
{
    public class ExaminationDonor
    {
        public string DonorId { get; set; }

        public Donor Donor { get; set; }

        public string ExaminationId { get; set; }

        public Examination Examination { get; set; }
    }
}
