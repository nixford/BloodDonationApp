namespace BloodDonationApp.Web.Areas.Administration.Controllers
{
    using BloodDonationApp.Common;
    using BloodDonationApp.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
