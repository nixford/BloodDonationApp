namespace BloodDonationApp.Data.Models
{
    using BloodDonationApp.Data.Common.Models;

    public class Examination : BaseDeletableModel<string>
    {
        public bool HepatitisC { get; set; }

        public bool HepatitisB { get; set; }

        public bool HIV { get; set; }

        public bool Syphilis { get; set; }
    }
}
