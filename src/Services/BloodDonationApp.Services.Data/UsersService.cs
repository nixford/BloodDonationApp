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
        private readonly IDeletableEntityRepository<Location> locationRepository;

        public UsersService(
            IDeletableEntityRepository<ApplicationRole> roleRepository,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.roleRepository = roleRepository;
            this.userManager = userManager;
            this.userRepository = userRepository;
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
