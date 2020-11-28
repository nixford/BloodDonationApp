﻿namespace BloodDonationApp.Services.Data
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
        private readonly IDeletableEntityRepository<Recipient> recipientRepository;

        public HospitalsService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<HospitalData> hospitalsRepository,
            IDeletableEntityRepository<ApplicationUserHospitalData> appUsersHospitalRepository,
            IDeletableEntityRepository<ApplicationRole> rolesRepository,
            IDeletableEntityRepository<Recipient> recipientRepository)
        {
            this.usersRepository = usersRepository;
            this.hospitalsRepository = hospitalsRepository;
            this.appUsersHospitalRepository = appUsersHospitalRepository;
            this.rolesRepository = rolesRepository;
            this.recipientRepository = recipientRepository;
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

        public IEnumerable<T> GetAllHospitals<T>(int? take = null, int skip = 0)
        {
            var hospitalDatas = this.hospitalsRepository.All()
                .OrderByDescending(hd => hd.CreatedOn)
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
