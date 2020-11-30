namespace BloodDonationApp.Web.ViewModels.Donor
{
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;

    public class DonorsInfoViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
