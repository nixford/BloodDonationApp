using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BloodDonationApp.Web.Areas.Identity.IdentityHostingStartup))]

namespace BloodDonationApp.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}