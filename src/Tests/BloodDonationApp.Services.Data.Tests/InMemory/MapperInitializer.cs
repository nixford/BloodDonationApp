namespace BloodDonationApp.Services.Data.Tests.InMemory
{
    using System.Reflection;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;
    using BloodDonationApp.Web.ViewModels.Donor;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(DonorDataProfileInputModel).GetTypeInfo().Assembly,
                typeof(DonorData).GetTypeInfo().Assembly);
        }
    }
}
