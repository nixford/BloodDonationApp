namespace BloodDonationApp.Data.Models
{
    using System;

    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class Disease : BaseDeletableModel<string>
    {
        public Disease()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string DiseaseName { get; set; }

        public string DiseaseDescription { get; set; }

        public DiseaseStatus DiseaseStatus { get; set; }
    }
}
