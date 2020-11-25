namespace BloodDonationApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;

    public class BloodBanksService : IBloodBanksService
    {
        private readonly IDeletableEntityRepository<BloodBank> bloodBankRepository;
        private readonly IDeletableEntityRepository<BloodBag> bloodBangRepository;
        private readonly IDeletableEntityRepository<HospitalData> hospitalDataRepository;

        public BloodBanksService(
            IDeletableEntityRepository<BloodBank> bloodBankRepository,
            IDeletableEntityRepository<BloodBag> bloodBangRepository, 
            IDeletableEntityRepository<HospitalData> hospitalDataRepository)
        {
            this.bloodBankRepository = bloodBankRepository;
            this.bloodBangRepository = bloodBangRepository;
            this.hospitalDataRepository = hospitalDataRepository;
        }

        public IEnumerable<BloodBag> GetHospitalBloodBagsById(params string[] id)
        {
            var hospitalData = this.hospitalDataRepository.All()
                .FirstOrDefault(hd => hd.ApplicationUserId == id[0]);

            var bloodBank = this.bloodBankRepository
                .All()
                .Where(bb => hospitalData != null ? bb.HospitalDataId == hospitalData.Id :
                bb.HospitalDataId == id[0])
                .FirstOrDefault();

            var bloodBags = this.bloodBangRepository.All()
                .Where(bb => bb.BloodBankId == bloodBank.Id)
                .ToList();

            return bloodBags;
        }        
    }
}
