namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class ExaminationDonor : BaseDeletableModel<string>
    {
        public ExaminationDonor()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string DonorDataId { get; set; }

        public DonorData DonorData { get; set; }

        public string ExaminationId { get; set; }

        public Examination Examination { get; set; }
    }
}
