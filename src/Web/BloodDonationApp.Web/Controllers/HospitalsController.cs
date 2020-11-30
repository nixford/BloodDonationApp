namespace BloodDonationApp.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.BloodBank;
    using BloodDonationApp.Web.ViewModels.Hospital;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HospitalsController : BaseController
    {
        private readonly IHospitalsService hospitalsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRecipientsService recipientsService;
        private readonly IBloodBanksService bloodBanksService;
        private readonly IUsersService usersService;
        private const int take = 5;

        public HospitalsController(
            IHospitalsService hospitalsService,
            UserManager<ApplicationUser> userManager,
            IRecipientsService recipientsService,
            IBloodBanksService bloodBanksService,
            IUsersService usersService)
        {
            this.hospitalsService = hospitalsService;
            this.userManager = userManager;
            this.recipientsService = recipientsService;
            this.bloodBanksService = bloodBanksService;
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult AddHospital()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = this.usersService.GetUserById<HospitalProfileInputModel>(userId);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddHospital(HospitalProfileInputModel inputModel)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.hospitalsService.CreateHospitalProfileAsync(inputModel, userId);

            return this.RedirectToAction("AllHospRecip", "Recipients");
        }

        public IActionResult AllHospitals(AllHospitalsViewModel viewModel, int page = 1)
        {
            viewModel.Hospitals = this.hospitalsService.GetAllHospitals<HospitalInfoViewModel>(take, (int)(page - 1) * take);

            var count = this.hospitalsService.GetAllHospitalsCount<HospitalInfoViewModel>().Count();

            if (!string.IsNullOrEmpty(viewModel.SearchTerm))
            {
                viewModel.Hospitals = this.hospitalsService
                    .GetAllHospitals<HospitalInfoViewModel>(take, (int)(page - 1) * take)
                    .Where(h => h.Name.Contains(viewModel.SearchTerm)
                    || h.Location.Country.Contains(viewModel.SearchTerm)
                    || h.Location.AdressDescription.Contains(viewModel.SearchTerm));

                count = viewModel.Hospitals.Count();
            }

            viewModel.PagesCount = (int)Math.Ceiling((double)count / take);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;
            return this.View(viewModel);
        }

        public IActionResult DetailsHospital(string hospitalDataId, AllHospitalBloodBagsViewModel viewModel)
        {
            viewModel.HospitalInfo = this.hospitalsService.GetHospitalDataById<HospitalInfoViewModel>(hospitalDataId);

            var allBags = this.bloodBanksService.GetHospitalBloodBagsById(hospitalDataId);

            foreach (var bag in allBags)
            {
                if ((int)bag.BloodGroup == 0)
                {
                    if (bag.RhesusFactor == 0)
                    {
                        viewModel.ABPositiveQuantity += bag.Quantity;
                    }
                    else
                    {
                        viewModel.ABNegativeQuantity += bag.Quantity;
                    }
                }
                else if ((int)bag.BloodGroup == 1)
                {
                    if (bag.RhesusFactor == 0)
                    {
                        viewModel.APositiveQuantity += bag.Quantity;
                    }
                    else
                    {
                        viewModel.ANegativeQuantity += bag.Quantity;
                    }
                }
                else if ((int)bag.BloodGroup == 2)
                {
                    if (bag.RhesusFactor == 0)
                    {
                        viewModel.BPositiveQuantity += bag.Quantity;
                    }
                    else
                    {
                        viewModel.BNegativeQuantity += bag.Quantity;
                    }
                }
                else if ((int)bag.BloodGroup == 3)
                {
                    if (bag.RhesusFactor == 0)
                    {
                        viewModel.ZeroPositiveQuantity += bag.Quantity;
                    }
                    else
                    {
                        viewModel.ZeroNegativeQuantity += bag.Quantity;
                    }
                }
            }

            return this.View(viewModel);
        }

        public IActionResult Contacts(string hospitalDataId, HospitalInfoViewModel viewModel)
        {
            var hospital = this.hospitalsService.GetHospitalDataById<HospitalInfoViewModel>(hospitalDataId);

            viewModel.Contact = new Contact
            {
                Phone = hospital.Contact.Phone,
                Email = hospital.Contact.Email,
            };

            viewModel.Location = new Location
            {
                Country = hospital.Location.Country,
                City = hospital.Location.City,
                AdressDescription = hospital.Location.AdressDescription,
            };

            return this.View(viewModel);
        }
    }
}
