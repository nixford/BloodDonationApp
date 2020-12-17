﻿namespace BloodDonationApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BloodDonationApp.Common;
    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;

    public class BloodBanksService : IBloodBanksService
    {
        private readonly IDeletableEntityRepository<BloodBank> bloodBankRepository;
        private readonly IDeletableEntityRepository<BloodBag> bloodBagRepository;
        private readonly IDeletableEntityRepository<HospitalData> hospitalDataRepository;

        public BloodBanksService(
            IDeletableEntityRepository<BloodBank> bloodBankRepository,
            IDeletableEntityRepository<BloodBag> bloodBagRepository,
            IDeletableEntityRepository<HospitalData> hospitalDataRepository)
        {
            this.bloodBankRepository = bloodBankRepository;
            this.bloodBagRepository = bloodBagRepository;
            this.hospitalDataRepository = hospitalDataRepository;
        }

        public IEnumerable<BloodBag> GetHospitalBloodBagsById(string id)
        {
            var hospitalData = this.hospitalDataRepository
                .All()
                .FirstOrDefault(hd => hd.ApplicationUserId == id);

            var bloodBank = this.bloodBankRepository
                .All()
                .Where(bb => hospitalData != null ? bb.HospitalDataId == hospitalData.Id :
                bb.HospitalDataId == id)
                .FirstOrDefault();

            if (bloodBank == null)
            {
                throw new ArgumentException(GlobalConstants.NoBloodBankDataErrorMessage);
            }

            var bloodBags = this.bloodBagRepository.All()
                .Where(bb => bb.BloodBankId == bloodBank.Id)
                .ToList();

            return bloodBags;
        }
    }
}
