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
    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationRole> roleRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<HospitalData> hospitalDataRepository;
        private readonly IDeletableEntityRepository<DonorData> donorDataRepository;
        private readonly IDeletableEntityRepository<Recipient> recipientsRepository;
        private readonly IDeletableEntityRepository<Request> requestsRepository;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ApplicationRole> roleRepository,
            IDeletableEntityRepository<HospitalData> hospitalDataRepository,
            IDeletableEntityRepository<DonorData> donorDataRepository,
            IDeletableEntityRepository<Recipient> recipientsRepository,
            IDeletableEntityRepository<Request> requestsRepository)
        {
            this.roleRepository = roleRepository;
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.hospitalDataRepository = hospitalDataRepository;
            this.donorDataRepository = donorDataRepository;
            this.recipientsRepository = recipientsRepository;
            this.requestsRepository = requestsRepository;
        }

        public IEnumerable<ApplicationUser> GetAllAdmins()
        {
            var adminRole = this.roleRepository
                .All()
                .FirstOrDefault(r => r.Name == GlobalConstants.AdministratorRoleName);

            var users = this.userRepository
                .All()
                .Where(u => u.IsDeleted == false
                && u.Roles.Any(r => r.RoleId == adminRole.Id))
                .ToList();

            return users;
        }

        public IEnumerable<ApplicationUser> GetAllDeletedUsers(int? take = null, int skip = 0)
        {
            var users = this.userRepository
                .AllWithDeleted()
                .OrderByDescending(u => u.CreatedOn)
                .Where(u => u.IsDeleted == true)
                .Skip(skip);

            if (take.HasValue)
            {
                users = users.Take(take.Value);
            }

            return users.ToList();
        }

        public T GetUserById<T>(string id)
        {
            if (id == null)
            {
                throw new ArgumentException(GlobalConstants.NoUserIdErrorMessage);
            }

            var user = this.userRepository.All()
                 .Where(u => u.Id == id)
                 .To<T>()
                 .FirstOrDefault();

            return user;
        }

        public T GetUserByName<T>(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentException(GlobalConstants.NoUserNameErrorMessage);
            }

            return this.userRepository.All()
               .Where(u => u.UserName == userName)
               .To<T>()
               .FirstOrDefault();
        }

        public string GetUserEmailById(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentException(GlobalConstants.NoUserIdErrorMessage);
            }

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

        public async Task<string> RemoveUserAsync(string email)
        {
            if (email == null)
            {
                throw new ArgumentException(GlobalConstants.NoUserEmailErrorMessage);
            }

            var user = this.userRepository.All()
                .Where(u => u.Email == email && u.IsDeleted == false)
                .FirstOrDefault();

            if (user == null)
            {
                throw new ArgumentNullException();
            }

            var donorData = this.donorDataRepository
                .All()
                .FirstOrDefault(dd => dd.ApplicationUserId == user.Id);

            var hospitalData = this.hospitalDataRepository
                .All()
                .FirstOrDefault(hd => hd.ApplicationUserId == user.Id);

            var hospitalRecipients = this.recipientsRepository
                .All()
                .Where(hr => hr.HospitalDataId == hospitalData.Id);

            var hospitalRequests = this.requestsRepository
                .All()
                .Where(hq => hq.HospitalId == hospitalData.Id);

            // Deleting user
            user.IsDeleted = true;
            user.DeletedOn = DateTime.UtcNow;
            await this.userRepository.SaveChangesAsync();

            // If the User is hospital - deleting hospitalData, recipients and requests
            if (hospitalData != null)
            {
                hospitalData.IsDeleted = true;
                hospitalData.DeletedOn = DateTime.UtcNow;
                await this.hospitalDataRepository.SaveChangesAsync();

                foreach (var recipient in hospitalRecipients)
                {
                    recipient.IsDeleted = true;
                    recipient.DeletedOn = DateTime.UtcNow;
                }

                await this.recipientsRepository.SaveChangesAsync();

                foreach (var request in hospitalRequests)
                {
                    request.IsDeleted = true;
                    request.DeletedOn = DateTime.UtcNow;
                }

                await this.requestsRepository.SaveChangesAsync();
            }

            // If the User is donor - deleting donorData
            if (donorData != null)
            {
                donorData.IsDeleted = true;
                donorData.DeletedOn = DateTime.UtcNow;
                await this.donorDataRepository.SaveChangesAsync();
            }

            return user.UserName;
        }

        public object GetUserEmailById(object id)
        {
            throw new NotImplementedException();
        }
    }
}
