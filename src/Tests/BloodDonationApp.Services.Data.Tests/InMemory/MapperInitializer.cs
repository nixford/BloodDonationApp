namespace BloodDonationApp.Services.Data.Tests.InMemory
{
    using System.Reflection;

    using BloodDonationApp.Services.Mapping;
    using BloodDonationApp.Web.ViewModels.Donor;
    using BloodDonationApp.Web.ViewModels.Hospital;
    using BloodDonationApp.Web.ViewModels.Message;
    using BloodDonationApp.Web.ViewModels.Recipient;
    using BloodDonationApp.Web.ViewModels.Request;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(DonorDataProfileInputModel).GetTypeInfo().Assembly,
                typeof(HospitalProfileInputModel).GetTypeInfo().Assembly,
                typeof(MessageViewModel).GetTypeInfo().Assembly,
                typeof(RecipientInfoViewModel).GetTypeInfo().Assembly,
                typeof(RequestInfoViewModel).GetTypeInfo().Assembly);
        }
    }
}
