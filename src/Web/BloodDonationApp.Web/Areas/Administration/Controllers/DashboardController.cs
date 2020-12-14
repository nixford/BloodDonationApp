namespace BloodDonationApp.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Common;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.Administration.Dashboard;
    using BloodDonationApp.Web.ViewModels.Administration.Users;
    using BloodDonationApp.Web.ViewModels.Donor;
    using BloodDonationApp.Web.ViewModels.Hospital;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private const string RegisterAdminSeccessMessage = "Successfull registration of a new administrator: {0}!";
        private const string RegisterAdminErrorMessage = "Error: The registration failed!";
        private const string RemoveSuccessMessage = "Successfully removed the {0} {1}!";
        private const string RemoveErrorMessage = "Error: Missing {0} with email {1}!";

        private readonly IUsersService usersService;
        private readonly IDonorsService donorsService;
        private readonly IHospitalsService hospitalsService;
        private const int take = 3;

        public DashboardController(
            IUsersService usersService,
            IDonorsService donorsService, 
            IHospitalsService hospitalsService)
        {
            this.usersService = usersService;
            this.donorsService = donorsService;
            this.hospitalsService = hospitalsService;
        }

        [HttpGet]
        public IActionResult Index(IndexViewModel viewModel, int page = 1)
        {
            viewModel.DonorsCount = this.donorsService
                .GetAllDonors<DonorsInfoViewModel>().Count();

            viewModel.HospitalsCount = this.hospitalsService
                .GetAllHospitals<HospitalInfoViewModel>().Count();

            viewModel.AdminsCount = this.usersService.GetAllAdmins()
                .Count();

            // Impplementing search and pagination for deleted users
            viewModel.DeletedUsers = this.usersService
                .GetAllDeletedUsers(take, (int)(page - 1) * take);

            var count = this.usersService.GetAllDeletedUsers().Count();

            if (!string.IsNullOrEmpty(viewModel.SearchTerm))
            {
                viewModel.DeletedUsers = this.usersService
                    .GetAllDeletedUsers()
                    .Where(u => u.UserName.Contains(viewModel.SearchTerm)
                    || u.Email.Contains(viewModel.SearchTerm));

                count = viewModel.DeletedUsers.Count();
            }

            viewModel.PagesCount = (int)Math.Ceiling((double)count / take);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult RemoveDonor()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveDonor(RemoveUserInputModel inputModel)
        {
            try
            {
                string donorName = await this.usersService.RemoveUserAsync(inputModel.Email);
                this.TempData["Msg"] = string.Format(RemoveSuccessMessage, GlobalConstants.DonorRoleName, donorName);
            }
            catch
            {
                this.TempData["Msg"] = string.Format(RemoveErrorMessage, GlobalConstants.DonorRoleName, inputModel.Email);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public IActionResult RemoveHospital()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveHospital(RemoveUserInputModel inputModel)
        {
            try
            {
                string hospitalName = await this.usersService.RemoveUserAsync(inputModel.Email);
                this.TempData["Msg"] = string.Format(RemoveSuccessMessage, GlobalConstants.HospitaltRoleName, hospitalName);
            }
            catch
            {
                this.TempData["Msg"] = string.Format(RemoveErrorMessage, GlobalConstants.HospitaltRoleName, inputModel.Email);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(RegisterAdminInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                if (!await this.usersService.AddAdmin(inputModel.UserName, inputModel.Email, inputModel.Password))
                {
                    this.TempData["Error"] = RegisterAdminErrorMessage;
                    return this.View(inputModel);
                }

                this.TempData["Success"] = string.Format(RegisterAdminSeccessMessage, inputModel.UserName);
            }
            catch (ArgumentException ex)
            {
                this.TempData["Error"] = ex.Message;
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public IActionResult RemoveAdmin()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdmin(RemoveUserInputModel inputModel)
        {
            try
            {
                string adminName = await this.usersService.RemoveUserAsync(inputModel.Email);
                this.TempData["Success"] = string.Format(RemoveSuccessMessage, GlobalConstants.AdministratorRoleName, adminName);
            }
            catch
            {
                this.TempData["Error"] = string.Format(RemoveErrorMessage, GlobalConstants.AdministratorRoleName, inputModel.Email);
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
