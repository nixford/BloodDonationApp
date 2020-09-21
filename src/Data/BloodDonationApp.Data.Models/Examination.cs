namespace BloodDonationApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BloodDonationApp.Data.Common.Models;

    public class Examination : BaseDeletableModel<string>
    {
        public Examination()
        {
            this.Diseases = new HashSet<Disease>();
        }

        public DateTime ExaminationDate { get; set; }

        public virtual ICollection<Disease> Diseases { get; set; }
    }
}
