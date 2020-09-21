namespace BloodDonationApp.Data.Models
{
    using BloodDonationApp.Data.Models.Enums;

    public class Disease
    {
        public DiseasesType DiseasesType { get; set; }

        public DiseaseStatus DiseaseStatus { get; set; }
    }
}
