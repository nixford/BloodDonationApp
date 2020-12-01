namespace BloodDonationApp.Web.ViewModels.Settings
{
    using AutoMapper;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;

    public class SettingViewModel : IMapTo<Setting>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string NameAndValue { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Setting, SettingViewModel>().ForMember(
                m => m.NameAndValue,
                opt => opt.MapFrom(x => x.Name + " = " + x.Value));
        }
    }
}
