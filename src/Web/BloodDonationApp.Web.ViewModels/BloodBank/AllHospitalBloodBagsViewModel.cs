﻿namespace BloodDonationApp.Web.ViewModels.BloodBank
{
    using System.Collections.Generic;

    using BloodDonationApp.Data.Models;

    public class AllHospitalBloodBagsViewModel
    {
        public double ABPositiveQuantity { get; set; }

        public double ABNegativeQuantity { get; set; }

        public double APositiveQuantity { get; set; }

        public double ANegativeQuantity { get; set; }

        public double BPositiveQuantity { get; set; }

        public double BNegativeQuantity { get; set; }

        public double ZeroPositiveQuantity { get; set; }

        public double ZeroNegativeQuantity { get; set; }
    }
}
