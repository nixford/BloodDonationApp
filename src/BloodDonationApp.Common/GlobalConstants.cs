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
        public const string NoDonorDataErrorMessage = "No donor data available!";
        public const string NoHospitalDataErrorMessage = "No hospital data available!";
        public const string NoBloodBankDataErrorMessage = "No blood bank data available!";
        public const string NotAvailableEmailErrorMessage = "The email is already in use! Please try another one.";
        public const string NotMessageErrorMessage = "There is no message with this id!";
        public const string NotFullRecipientDataErrorMessage = "The recipient data is not full!";
        public const string NotRecipientDataErrorMessage = "The recipient dont't exist!";
        public const string RecipentIdConnotBeNullErrorMessage = "Recipent id can not be null!";
        public const string ExistingRequestForThisRecipient = "Request for this recipient already exist!";
        public const string NoQuantityErrrorMessage = "No blood quantity for the request!";
        public const string NoRequestErrorMessage = "No such request with this id!";
        public const string NoUserIdErrorMessage = "User id can not be null!";
        public const string NoUserNameErrorMessage = "User name can not be null!";
        public const string NoUserEmailErrorMessage = "User email can not be null!";
        public const string NoMessageIdErrorMessage = "Message id can not be null!";

        public const string MissingMessageElementErrorMessage = "The message should have content, authorId, userName and email!";

        public const int TopDonorsNumber = 10;

        private const string DeleteSuccessMessage = "You have successfully deleted the request for donation!";
    }
}
