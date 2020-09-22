namespace BloodDonationApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BloodDonationApp.Data.Common.Models;

    public class Examination : BaseModel<string>
    {
        public Examination()
        {
            this.Diseases = new HashSet<Disease>();
            this.ExaminationsDonors = new HashSet<ExaminationDonor>();
        }

        public DateTime ExaminationDate { get; set; }

        public virtual ICollection<Disease> Diseases { get; set; }

        public virtual ICollection<ExaminationDonor> ExaminationsDonors { get; set; }
    }
}
