namespace BloodDonationApp.Web.ViewModels.Request
{
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;

    public class RequestInfoViewModel : IMapFrom<ApplicationUser>
    {
        public string HospitalName { get; set; }

        public string Content { get; set; }

        public string PublishedOn { get; set; }

        public string EmergencyStatus { get; set; }

        public string BloodGroup { get; set; }

        public string RhesusFactor { get; set; }

        public double NeededQuantity { get; set; }
    }
}
