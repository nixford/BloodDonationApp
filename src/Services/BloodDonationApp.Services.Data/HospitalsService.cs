namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;
    using BloodDonationApp.Web.ViewModels.Hospital;

    public class HospitalsService : IHospitalsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<HospitalData> hospitalsRepository;
        private readonly IDeletableEntityRepository<ApplicationUserHospitalData> appUsersHospitalRepository;
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepository;

        public HospitalsService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<HospitalData> hospitalsRepository,
            IDeletableEntityRepository<ApplicationUserHospitalData> appUsersHospitalRepository,
            IDeletableEntityRepository<ApplicationRole> rolesRepository)
        {
            this.usersRepository = usersRepository;
            this.hospitalsRepository = hospitalsRepository;
            this.appUsersHospitalRepository = appUsersHospitalRepository;
            this.rolesRepository = rolesRepository;
        }

        public async Task CreateHospitalProfileAsync(HospitalProfileInputModel input, string userId)
        {
            var user = this.usersRepository.All()
                            .Where(u => u.Id == userId)
                            .FirstOrDefault();

            var hospitalData = new HospitalData
            {
                ApplicationUserId = userId,
                Name = input.Name,
                Contact = new Contact
                {
                    Phone = input.Phone,
                    Email = input.Email,
                },
                Location = new Location
                {
                    Country = input.Country,
                    City = input.City,
                    AdressDescription = input.AdressDescription,
                },
            };

            await this.hospitalsRepository.AddAsync(hospitalData);
            await this.hospitalsRepository.SaveChangesAsync();

            var appUserHospitalData = new ApplicationUserHospitalData
            {
                ApplicationUserId = userId,
                HospitalDataId = hospitalData.Id,
            };

            await this.appUsersHospitalRepository.AddAsync(appUserHospitalData);
            await this.appUsersHospitalRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllHospitals<T>()
        {
            var roleObj = this.rolesRepository.All()
                .FirstOrDefault(r => r.Name == "Hospital");

            var hospitals = this.usersRepository.All()
                .Where(u => u.Roles.All(r => r.RoleId == roleObj.Id));

            return hospitals.To<T>().ToList();
        }

        public T GetHospitalDataById<T>(string userHospitalId)
        {
            var userHospital = this.usersRepository
                .All()
                .Where(u => u.Id == userHospitalId)
                .FirstOrDefault();

            var hospitalData = this.hospitalsRepository
                .All()
                .Where(hd => hd.ApplicationUserId == userHospital.Id)
                .To<T>()
                .FirstOrDefault();

            return hospitalData;
        }

        public T GetHospitalDataByName<T>(string hospitalName)
        {
            return this.hospitalsRepository.All()
               .Where(h => h.Name == hospitalName)
               .To<T>()
               .FirstOrDefault();
        }
    }
}
