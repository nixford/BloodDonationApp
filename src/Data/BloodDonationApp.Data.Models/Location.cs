namespace BloodDonationApp.Data.Models
{
    using BloodDonationApp.Data.Common.Models;

    public class Location : BaseDeletableModel<string>
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string AdressDescription { get; set; }
    }
}
