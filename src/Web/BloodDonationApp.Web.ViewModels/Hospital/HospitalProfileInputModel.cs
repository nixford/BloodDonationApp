namespace BloodDonationApp.Web.ViewModels.Hospital
{
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;

    public class HospitalProfileInputModel : IMapFrom<ApplicationUser>
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string AdressDescription { get; set; }
    }
}
