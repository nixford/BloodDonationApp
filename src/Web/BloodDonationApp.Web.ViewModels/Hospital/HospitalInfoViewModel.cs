namespace BloodDonationApp.Web.ViewModels.Hospital
{

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;

    public class HospitalInfoViewModel : IMapFrom<HospitalData>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int RecipientCount { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual Location Location { get; set; }
    }
}