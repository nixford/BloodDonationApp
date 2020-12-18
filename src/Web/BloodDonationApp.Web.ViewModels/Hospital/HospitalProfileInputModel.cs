namespace BloodDonationApp.Web.ViewModels.Hospital
{
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class HospitalProfileInputModel : IMapTo<ApplicationUser>, IMapTo<HospitalData>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Country { get; set; }

        public string City { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string AdressDescription { get; set; }
    }
}
