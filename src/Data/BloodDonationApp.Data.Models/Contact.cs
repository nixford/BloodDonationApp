namespace BloodDonationApp.Data.Models
{
    using BloodDonationApp.Data.Common.Models;

    public class Contact : BaseModel<string>
    {
        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
