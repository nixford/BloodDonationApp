﻿namespace BloodDonationApp.Web.ViewModels.Administration.Users
{
    using System.ComponentModel.DataAnnotations;

    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;

    public class RegisterAdminInputModel : IMapTo<ApplicationUser>
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The Username must be between 3 and 30 characters long.", MinimumLength = 3)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Password must be between 6 and 100 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The confirmation password do not match with your password.")]
        public string ConfirmPassword { get; set; }
    }
}
