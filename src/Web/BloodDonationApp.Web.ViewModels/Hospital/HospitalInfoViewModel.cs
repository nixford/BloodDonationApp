namespace BloodDonationApp.Web.ViewModels.Hospital
{
    using AutoMapper;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;
    using BloodDonationApp.Web.ViewModels.Recipient;

    public class HospitalInfoViewModel : IMapFrom<HospitalData>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int RecipientCount { get; set; }

    }
}