namespace BloodDonationApp.Data.Models
{
    using BloodDonationApp.Data.Common.Models;

    public class Contact : BaseDeletableModel<string>
    {
        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
