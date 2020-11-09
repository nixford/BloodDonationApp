namespace BloodDonationApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class Examination : BaseDeletableModel<string>
    {
        public Examination()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime ExaminationDate { get; set; }

        [ForeignKey(nameof(Disease))]
        public string DiseaseId { get; set; }

        public virtual Disease Disease { get; set; }
    }
}
