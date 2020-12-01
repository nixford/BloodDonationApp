namespace BloodDonationApp.Web.ViewModels.Message
{
    using System.ComponentModel.DataAnnotations;

    using BloodDonationApp.Data.Models;

    public class MessageInputViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
    }
}
