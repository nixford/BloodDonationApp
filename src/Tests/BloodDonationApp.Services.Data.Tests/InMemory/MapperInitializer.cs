namespace BloodDonationApp.Services.Data.Tests.InMemory
{
    using System.Reflection;

    using BloodDonationApp.Services.Mapping;
    using BloodDonationApp.Web.ViewModels.Donor;
    using BloodDonationApp.Web.ViewModels.Hospital;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(DonorDataProfileInputModel).GetTypeInfo().Assembly,
                typeof(HospitalProfileInputModel).GetTypeInfo().Assembly);
        }
    }
}
