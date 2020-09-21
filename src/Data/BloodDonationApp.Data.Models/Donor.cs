﻿namespace BloodDonationApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class Donor : BaseDeletableModel<string>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public virtual Contact PhoneAndEmailDonor { get; set; }

        public virtual Location DonorLocation { get; set; }

        public virtual EmergencyStatus DonorAveilableStatus { get; set; }

        public virtual BloodType BloodType { get; set; }
    }
}
