namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;
    using BloodDonationApp.Web.ViewModels.Hospital;
    using Microsoft.AspNetCore.Mvc;

    public class HospitalsService : IHospitalsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<HospitalData> hospitalsRepository;
        private readonly IDeletableEntityRepository<ApplicationUserHospitalData> appUsersHospitalRepository;
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepository;
        private readonly IDeletableEntityRepository<Recipient> recipientRepository;
        private readonly IDeletableEntityRepository<BloodBank> bloodBankRepository;
        private readonly IDeletableEntityRepository<BloodBag> bagRepository;
        private readonly IDeletableEntityRepository<HospitalDataBloodBank> hospitalBloodBankRepository;

        public HospitalsService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<HospitalData> hospitalsRepository,
            IDeletableEntityRepository<ApplicationUserHospitalData> appUsersHospitalRepository,
            IDeletableEntityRepository<ApplicationRole> rolesRepository,
            IDeletableEntityRepository<Recipient> recipientRepository,
            IDeletableEntityRepository<BloodBank> bloodBankRepository,
            IDeletableEntityRepository<HospitalDataBloodBank> hospitalBloodBankRepository, 
            IDeletableEntityRepository<BloodBag> bagRepository)
        {
            this.usersRepository = usersRepository;
            this.hospitalsRepository = hospitalsRepository;
            this.appUsersHospitalRepository = appUsersHospitalRepository;
            this.rolesRepository = rolesRepository;
            this.recipientRepository = recipientRepository;
            this.bloodBankRepository = bloodBankRepository;
            this.hospitalBloodBankRepository = hospitalBloodBankRepository;
            this.bagRepository = bagRepository;
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

            var bloodBank = new BloodBank
            {
                HospitalDataId = hospitalData.Id,
            };
            await this.bloodBankRepository.AddAsync(bloodBank);
            await this.bloodBankRepository.SaveChangesAsync();

            var bag = new BloodBag
            {
                BloodBankId = bloodBank.Id,
            };
            await this.bagRepository.AddAsync(bag);
            await this.bagRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllHospitals<T>(int? take = null, int skip = 0)
        {
            var hospitalDatas = this.hospitalsRepository.All()
                .OrderByDescending(hd => hd.CreatedOn)
                .Where(hd => hd.IsDeleted == false)
                .Select(hd => new HospitalData
                {
                    Id = hd.Id,
                    Name = hd.Name,
                    RecipientCount = this.recipientRepository.All()
                                    .Where(r => r.HospitalDataId == hd.Id)
                                    .Count(),
                    Location = new Location
                    {
                        Country = hd.Location.Country,
                        City = hd.Location.City,
                        AdressDescription = hd.Location.AdressDescription,
                    },
                    Contact = new Contact
                    {
                        Phone = hd.Contact.Phone,
                        Email = hd.Contact.Email,
                    },
                })
                .Skip(skip);

            if (take.HasValue)
            {
                hospitalDatas = hospitalDatas.Take(take.Value);
            }

            return hospitalDatas.To<T>().ToList();
        }

        public IEnumerable<T> GetAllHospitalsCount<T>()
        {
            var hospitalDatas = this.hospitalsRepository
                .All()
                .Where(hd => hd.IsDeleted == false)
                .To<T>()
                .ToList();

            return hospitalDatas;
        }

        public T GetHospitalDataById<T>(string userHospitalOrHospitalDataId)
        {
            var userHospital = this.usersRepository
                .All()
                .Where(u => u.Id == userHospitalOrHospitalDataId)
                .FirstOrDefault();

            var hospitalData = this.hospitalsRepository
                .All()
                .Where(hd => userHospital != null ? hd.ApplicationUserId == userHospital.Id :
                    hd.Id == userHospitalOrHospitalDataId)
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
