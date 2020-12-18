namespace BloodDonationApp.Web.Controllers
{
    using System.Linq;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Data;
    using BloodDonationApp.Web.ViewModels.BloodBank;
    using BloodDonationApp.Web.ViewModels.Hospital;
    using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult HospitalBlBags(AllHospitalBloodBagsViewModel viewModel)
        {
            var userHospitalId = this.userManager.GetUserId(this.User);

            viewModel.HospitalInfo =
                this.hospitalsService
                .GetHospitalDataById<HospitalInfoViewModel>(userHospitalId, null);

            if (viewModel.HospitalInfo == null)
            {
                return this.RedirectToAction("HttpStatusCodeHandler", "Error", this.NotFound());
            }

            var allBags = this.bloodBanksService.GetHospitalBloodBagsById(userHospitalId);

            foreach (var bag in allBags)
            {
                if ((int)bag.BloodGroup == 0)
                {
                    if (bag.RhesusFactor == 0)
                    {
                        viewModel.ABPositiveQuantity += bag.Quantity;
                        viewModel.ABPositiveQuantity =
                            viewModel.ABPositiveQuantity > 10000 ? 10000 :
                            viewModel.ABPositiveQuantity;
                    }
                    else
                    {
                        viewModel.ABNegativeQuantity += bag.Quantity;
                        viewModel.ABNegativeQuantity =
                            viewModel.ABNegativeQuantity > 10000 ? 10000 :
                            viewModel.ABNegativeQuantity;
                    }
                }
                else if ((int)bag.BloodGroup == 1)
                {
                    if (bag.RhesusFactor == 0)
                    {
                        viewModel.APositiveQuantity += bag.Quantity;
                        viewModel.APositiveQuantity =
                            viewModel.APositiveQuantity > 10000 ? 10000 :
                            viewModel.APositiveQuantity;
                    }
                    else
                    {
                        viewModel.ANegativeQuantity += bag.Quantity;
                        viewModel.ANegativeQuantity =
                            viewModel.ANegativeQuantity > 10000 ? 10000 :
                            viewModel.ANegativeQuantity;
                    }
                }
                else if ((int)bag.BloodGroup == 2)
                {
                    if (bag.RhesusFactor == 0)
                    {
                        viewModel.BPositiveQuantity += bag.Quantity;
                        viewModel.BPositiveQuantity =
                            viewModel.BPositiveQuantity > 10000 ? 10000 :
                            viewModel.BPositiveQuantity;
                    }
                    else
                    {
                        viewModel.BNegativeQuantity += bag.Quantity;
                        viewModel.BNegativeQuantity =
                            viewModel.BNegativeQuantity > 10000 ? 10000 :
                            viewModel.BNegativeQuantity;
                    }
                }
                else if ((int)bag.BloodGroup == 3)
                {
                    if (bag.RhesusFactor == 0)
                    {
                        viewModel.ZeroPositiveQuantity += bag.Quantity;
                        viewModel.ZeroPositiveQuantity =
                            viewModel.ZeroPositiveQuantity > 10000 ? 10000 :
                            viewModel.ZeroPositiveQuantity;
                    }
                    else
                    {
                        viewModel.ZeroNegativeQuantity += bag.Quantity;
                        viewModel.ZeroNegativeQuantity =
                            viewModel.ZeroNegativeQuantity > 10000 ? 10000 :
                            viewModel.ZeroNegativeQuantity;
                    }
                }
            }

            return this.View(viewModel);
        }
    }
}
