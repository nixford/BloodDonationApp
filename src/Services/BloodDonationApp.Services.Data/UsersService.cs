namespace BloodDonationApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Common;
    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;
    using BloodDonationApp.Web.ViewModels.Donor;
    using BloodDonationApp.Web.ViewModels.Hospital;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<ApplicationRole> roleRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<DonorData> donorRepository;
        private readonly IDeletableEntityRepository<ApplicationUserDonorData> appUserDonorRepository;
        private readonly IDeletableEntityRepository<ApplicationUserHospitalData> appUserHospitalRepository;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<ApplicationRole> roleRepository,
            IDeletableEntityRepository<DonorData> donorRepository,
            IDeletableEntityRepository<ApplicationUserDonorData> appUserDonorRepository,
            IDeletableEntityRepository<ApplicationUserHospitalData> appUserHospitalRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.donorRepository = donorRepository;
            this.appUserDonorRepository = appUserDonorRepository;
            this.appUserHospitalRepository = appUserHospitalRepository;
            this.userManager = userManager;
        }

        public async Task CreateDonorProfileAsync(DonorDataProfileInputModel input, string userId)
        {
            var user = this.userRepository.All()
                            .Where(u => u.Id == userId)
                            .FirstOrDefault();

            var donorData = new DonorData
            {
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
                DonorAveilableStatus = input.DonorAveilableStatus,
                BloodType = new BloodType
                {
                    BloodGroup = input.BloodGroup,
                    RhesusFactor = input.RhesusFactor,
                },
                Examination = new Examination
                {
                    ExaminationDate = input.ExaminationDate,
                    Disease = new Disease
                    {
                        DiseaseName = input.DiseaseName,
                        DiseaseDescription = input.DiseaseDescription,
                        DiseaseStatus = input.DiseaseStatus,
                    },
                },
            };

            //await this.donorRepository.AddAsync(donorData);
            //await this.donorRepository.SaveChangesAsync();

            var appUserDonorData = new ApplicationUserDonorData
            {
                ApplicationUserId = userId,
                DonorDataId = donorData.Id,
            };

            await this.appUserDonorRepository.AddAsync(appUserDonorData);
            await this.appUserDonorRepository.SaveChangesAsync();
        }

        public async Task CreateHospitalProfileAsync(HospitalProfileInputModel input, string userId)
        {
            var user = this.userRepository.All()
                            .Where(u => u.Id == userId)
                            .FirstOrDefault();

            var hospitalData = new HospitalData
            {
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

            var appUserHospitalData = new ApplicationUserHospitalData
            {
                ApplicationUserId = userId,
                HospitalDataId = hospitalData.Id,
            };

            await this.appUserHospitalRepository.AddAsync(appUserHospitalData);
            await this.appUserHospitalRepository.SaveChangesAsync();

            await this.appUserHospitalRepository.AddAsync(appUserHospitalData);
            await this.appUserHospitalRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllDonors<T>()
        {
            var roleObj = this.roleRepository.All()
                .FirstOrDefault(r => r.Name == "Donor");

            var donors = this.userRepository.All()
                .Where(u => u.Roles.All(r => r.RoleId == roleObj.Id));

            return donors.To<T>().ToList();
        }

        public IEnumerable<T> GetAllHospitals<T>()
        {
            var roleObj = this.roleRepository.All()
                .FirstOrDefault(r => r.Name == "Hospital");

            var hospitals = this.userRepository.All()
                .Where(u => u.Roles.All(r => r.RoleId == roleObj.Id));

            return hospitals.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var user = this.userRepository
                 .All()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefault();

            return user;
        }

        public IEnumerable<T> GetTopDonors<T>()
        {
            var topDonors = this.appUserDonorRepository.All()
                .OrderByDescending(u => u.DonorData.DonorsDonationEvents.Count)
                .Take(GlobalConstants.TopDonorsNumber);

            return topDonors.To<T>().ToList();
        }

        public T GetUserById<T>(string id)
        {
            var user = this.userRepository.All()
                 .Where(u => u.Id == id)
                 .To<T>()
                 .FirstOrDefault();

            return user;
        }

        public T GetUserByName<T>(string userName)
        {
            return this.userRepository.All()
               .Where(u => u.UserName == userName)
               .To<T>()
               .FirstOrDefault();
        }

        public string GetUserEmailById(string userId)
        {
            return this.userRepository.All()
                .Where(u => u.Id == userId)
                .Select(u => u.Email)
                .FirstOrDefault();
        }

        public async Task<bool> AddAdmin(string userName, string email, string password)
        {
            if (this.userRepository.All().Any(u => u.UserName == userName))
            {
                throw new ArgumentException(GlobalConstants.NotAvailableUserNameErrorMessage);
            }

            if (this.userRepository.All().Any(u => u.Email == email))
            {
                throw new ArgumentException(GlobalConstants.NotAvailableEmailErrorMessage);
            }

            var newAdmin = new ApplicationUser
            {
                UserName = userName,
                Email = email,
            };

            var result = await this.userManager.CreateAsync(newAdmin, password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(newAdmin, GlobalConstants.AdministratorRoleName);
            }

            return result.Succeeded;
        }

        public async Task<string> RemoveAdminAsync(string email, string role)
        {
            var user = this.userRepository.All()
                .Where(u => u.Email == email)
                .FirstOrDefault();

            var users = await this.userManager.GetUsersInRoleAsync(role);

            if (user == null || users.All(u => u.Email != user?.Email))
            {
                throw new ArgumentNullException();
            }

            user.IsDeleted = true;
            await this.userRepository.SaveChangesAsync();

            return user.UserName;
        }

        public async Task<string> RemoveUserAsync(string email)
        {
            var user = this.userRepository.All()
                .Where(u => u.Email == email && u.IsDeleted == false)
                .FirstOrDefault();

            if (user == null)
            {
                throw new ArgumentNullException();
            }

            user.IsDeleted = true;

            await this.userRepository.SaveChangesAsync();

            return user.UserName;
        }
    }
}
