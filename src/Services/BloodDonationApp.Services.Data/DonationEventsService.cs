namespace BloodDonationApp.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;

    public class DonationEventsService : IDonationEventsService
    {
        private readonly IDeletableEntityRepository<DonationEvent> donationEventRepository;
        private readonly IDeletableEntityRepository<BloodBank> bloodBankRepository;
        private readonly IDeletableEntityRepository<BloodBag> bloodBagRepository;
        private readonly IDeletableEntityRepository<HospitalData> hospitalDataUserRepository;
        private readonly IDeletableEntityRepository<DonorData> donorDataUserRepository;
        private readonly IDeletableEntityRepository<Request> requestRepository;

        public DonationEventsService(
            IDeletableEntityRepository<DonationEvent> donationEventRepository,
            IDeletableEntityRepository<BloodBank> bloodBankRepository,
            IDeletableEntityRepository<BloodBag> bloodBagRepository,
            IDeletableEntityRepository<HospitalData> hospitalDataUserRepository,
            IDeletableEntityRepository<DonorData> donorDataUserRepository,
            IDeletableEntityRepository<Request> requestRepository)
        {
            this.donationEventRepository = donationEventRepository;
            this.bloodBankRepository = bloodBankRepository;
            this.hospitalDataUserRepository = hospitalDataUserRepository;
            this.donorDataUserRepository = donorDataUserRepository;
            this.requestRepository = requestRepository;
            this.bloodBagRepository = bloodBagRepository;
        }

        public async Task CreateDonation(string requestOrHospitalId, string userDonorId)
        {
            var donorData = this.donorDataUserRepository.All()
                .FirstOrDefault(ddu => ddu.ApplicationUserId == userDonorId);

            var request = this.requestRepository.All()
                .FirstOrDefault(r => r.Id == requestOrHospitalId);

            var hospitalData = this.hospitalDataUserRepository.All()
                .FirstOrDefault(hd =>
                request != null ?
                hd.Id == request.HospitalId : hd.Id == requestOrHospitalId);

            var bloodBank = this.bloodBankRepository.All()
                .FirstOrDefault(bbk => bbk.HospitalDataId == hospitalData.Id);

            var donationEvent = new DonationEvent
            {
                DateOfDonation = DateTime.UtcNow,
                RequestId = request != null ? requestOrHospitalId : null,
                UserDonorId = userDonorId,
            };

            await this.donationEventRepository.AddAsync(donationEvent);
            await this.donationEventRepository.SaveChangesAsync();

            var currBloodBank = new BloodBank { };
            if (bloodBank == null)
            {
                currBloodBank.HospitalDataId = hospitalData.Id;

                await this.bloodBankRepository.AddAsync(currBloodBank);
                await this.bloodBankRepository.SaveChangesAsync();
            }

            var bloodBag = new BloodBag
            {
                Quantity = request != null ? request.NeededQuantity : 500,
                CollectionDate = donationEvent.DateOfDonation,
                DonorDataId = donorData.Id,
                BloodGroup = request != null ? request.BloodGroup : donorData.BloodGroup,
                RhesusFactor = request != null ? request.RhesusFactor : donorData.RhesusFactor,
                BloodBankId = bloodBank == null ? currBloodBank.Id : bloodBank.Id,
            };

            await this.bloodBagRepository.AddAsync(bloodBag);
            await this.bloodBagRepository.SaveChangesAsync();
        }
    }
}
