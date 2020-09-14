namespace BloodDonationApp.Data.Models
{
    using BloodDonationApp.Data.Common.Models;

    public class Donor : BaseDeletableModel<string>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Location DonorLocation { get; set; }

        // Available...
    }
}
