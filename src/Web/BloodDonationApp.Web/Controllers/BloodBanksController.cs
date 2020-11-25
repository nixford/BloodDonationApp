namespace BloodDonationApp.Web.Controllers
{
    using System.Linq;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.BloodBank;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BloodBanksController : BaseController
    {
        private readonly IBloodBanksService bloodBanksService;
        private readonly IHospitalsService hospitalsService;
        private readonly UserManager<ApplicationUser> userManager;

        public BloodBanksController(
            IBloodBanksService bloodBanksService,
            UserManager<ApplicationUser> userManager,
            IHospitalsService hospitalsService)
        {
            this.bloodBanksService = bloodBanksService;
            this.userManager = userManager;
            this.hospitalsService = hospitalsService;
        }

        public IActionResult HospitalBlBags(string hospitalDataId, AllHospitalBloodBagsViewModel viewModel)
        {
            var userHospitalId = this.userManager.GetUserId(this.User);

            var allBags = this.bloodBanksService.GetHospitalBloodBagsById(userHospitalId);

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

            var hospitalData = this.hospitalsService
                .GetAllHospitals<HospitalData>()
                .FirstOrDefault(hd => hd.ApplicationUserId == userHospitalId);

            viewModel.HospitalInfo.Location = new Location
            {
                Country = hospitalData.Location.Country,
                City = hospitalData.Location.City,
                AdressDescription = hospitalData.Location.AdressDescription,
            };

            return this.View(viewModel);
        }
    }
}
