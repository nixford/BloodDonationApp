namespace BloodDonationApp.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Web.ViewModels.DonationEvents;
    using BloodDonationApp.Web.ViewModels.Request;

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

        public async Task CreateDonation(
            string requestOrHospitalId,
            string userDonorId,
            RequestInfoViewModel model,
            DonationEventInputModel viewModel)
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

            var bag = this.bloodBagRepository.All()
                .FirstOrDefault(bg => bg.BloodBankId == bloodBank.Id);

            var donationEvent = new DonationEvent
            {
                DateOfDonation = DateTime.UtcNow,
                RequestId = request != null ? requestOrHospitalId : null,
                UserDonorId = userDonorId,
            };

            await this.donationEventRepository.AddAsync(donationEvent);
            await this.donationEventRepository.SaveChangesAsync();

            bag = new BloodBag
            {
                Quantity = viewModel.Quantity != 0 ? viewModel.Quantity : model.NeededQuantity,
                CollectionDate = DateTime.UtcNow,
                DonorDataId = donorData.Id,
                BloodGroup = viewModel.BloodGroup,
                RhesusFactor = viewModel.RhesusFactor,
                BloodBankId = bloodBank.Id,
            };

            await this.bloodBagRepository.AddAsync(bag);
            await this.bloodBagRepository.SaveChangesAsync();
        }
    }
}
