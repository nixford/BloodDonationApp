namespace BloodDonationApp.Web.Controllers
{
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

        public HospitalsController(
            IHospitalsService hospitalsService,
            UserManager<ApplicationUser> userManager,
            IRecipientsService recipientsService, 
            IBloodBanksService bloodBanksService)
        {
            this.hospitalsService = hospitalsService;
            this.userManager = userManager;
            this.recipientsService = recipientsService;
            this.bloodBanksService = bloodBanksService;
        }

        [Authorize]
        public IActionResult AddHospital()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = this.hospitalsService.GetHospitalDataById<HospitalProfileInputModel>(userId);

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

            return this.RedirectToAction("AllRequests", "Requests");
        }

        public IActionResult AllHospitals(AllHospitalsViewModel viewModel)
        {

            viewModel.Hospitals = this.hospitalsService.GetAllHospitals<HospitalInfoViewModel>();

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
