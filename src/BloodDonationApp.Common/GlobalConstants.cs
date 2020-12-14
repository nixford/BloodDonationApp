namespace BloodDonationApp.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "BloodDonationApp";

        public const string AdministratorRoleName = "Administrator";
        public const string DonorRoleName = "Donor";
        public const string HospitaltRoleName = "Hospital";

        public const string NotAvailableUserNameErrorMessage = "The username is already in use! Please, try another one.";
        public const string NoUserRegistrationErrorMessage = "You don't have registration as user!";
        public const string NotAvailableEmailErrorMessage = "The email is already in use! Please try another one.";

        public const int TopDonorsNumber = 10;

        private const string DeleteSuccessMessage = "You have successfully deleted the request for donation!";
    }
}
