namespace BloodDonationApp.Data.Models
{
    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class Disease : BaseDeletableModel<string>
    {
        public string DiseaseName { get; set; }

        public string Description { get; set; }

        public DiseaseStatus DiseaseStatus { get; set; }
    }
}
