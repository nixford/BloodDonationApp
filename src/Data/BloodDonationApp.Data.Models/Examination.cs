namespace BloodDonationApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class Examination : BaseDeletableModel<string>
    {
        public Examination()
        {
            this.ExaminationsDonors = new HashSet<ExaminationDonor>();
        }

        public DateTime ExaminationDate { get; set; }

        [ForeignKey(nameof(Disease))]
        public string DiseaseId { get; set; }

        public virtual Disease Disease { get; set; }

        public virtual ICollection<ExaminationDonor> ExaminationsDonors { get; set; }
    }
}
