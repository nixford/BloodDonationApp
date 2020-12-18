namespace BloodDonationApp.Web.ViewModels.Message
{
    using System.ComponentModel.DataAnnotations;

    using BloodDonationApp.Data.Models;

    public class MessageInputViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }
    }
}
