namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Common;
    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;
    using BloodDonationApp.Web.ViewModels.Donor;

    public class DonorsService : IDonorsService
    {
        private readonly IDeletableEntityRepository<DonorData> donorRepository;
        private readonly IDeletableEntityRepository<ApplicationUserDonorData> appUserDonorRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<ApplicationRole> roleRepository;

        public DonorsService(
            IDeletableEntityRepository<DonorData> donorRepository,
            IDeletableEntityRepository<ApplicationUserDonorData> appUserDonorRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<ApplicationRole> roleRepository)
        {
            this.donorRepository = donorRepository;
            this.appUserDonorRepository = appUserDonorRepository;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public async Task CreateDonorProfileAsync(DonorDataProfileInputModel input, string userId)
        {
            var user = this.userRepository.All()
                            .Where(u => u.Id == userId)
                            .FirstOrDefault();

            var donorData = new DonorData
            {
                ApplicationUserId = user.Id,
                FirstName = input.FirstName,
                MiddleName = input.MiddleName,
                LastName = input.LastName,
                Age = input.Age,
                Contact = new Contact
                {
                    Phone = input.PhoneNumber,
                    Email = user.Email,
                },
                Location = new Location
                {
                    Country = input.Country,
                    City = input.City,
                    AdressDescription = input.AdressDescription,
                },
                Examination = new Examination
                {
                    Disease = new Disease
                    {
                        DiseaseStatus = input.DiseaseStatus,
                        DiseaseName = input.DiseaseName,
                        DiseaseDescription = input.DiseaseDescription,
                    },
                },
                DonorAveilableStatus = input.DonorAveilableStatus,
                BloodGroup = input.BloodGroup,
                RhesusFactor = input.RhesusFactor,
            };

            await this.donorRepository.AddAsync(donorData);
            await this.donorRepository.SaveChangesAsync();

            var appUserDonorData = new ApplicationUserDonorData
            {
                ApplicationUserId = userId,
                DonorDataId = donorData.Id,
            };

            await this.appUserDonorRepository.AddAsync(appUserDonorData);
            await this.appUserDonorRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllDonors<T>()
        {
            var roleObj = this.roleRepository.All()
                .FirstOrDefault(r => r.Name == "Donor");

            var donors = this.userRepository
                .All()
                .Where(u => u.IsDeleted == false && u.Roles.All(r => r.RoleId == roleObj.Id))
                .To<T>()
                .ToList();

            return donors;
        }

        public IEnumerable<T> GetTopDonors<T>()
        {
            var topDonors = this.appUserDonorRepository
                .All()
                .OrderByDescending(u => u.DonorData.DonorAveilableStatus)
                .Where(u => u.IsDeleted == false)
                .Take(GlobalConstants.TopDonorsNumber);

            return topDonors.To<T>().ToList();
        }
    }
}
