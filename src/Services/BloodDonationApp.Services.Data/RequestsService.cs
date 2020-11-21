namespace BloodDonationApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;
    using BloodDonationApp.Web.ViewModels.Request;

    public class RequestsService : IRequestsService
    {
        private readonly IDeletableEntityRepository<Request> requestsRepository;
        private readonly IDeletableEntityRepository<HospitalDataRequest> requestsHospitalDataRepository;
        private readonly IDeletableEntityRepository<HospitalData> hospitalDataRepository;
        private readonly IDeletableEntityRepository<RecipientRequest> recipientRequesDataRepository;

        public RequestsService(
            IDeletableEntityRepository<Request> requestsRepository,
            IDeletableEntityRepository<HospitalDataRequest> requestsHospitalDataRepository,
            IDeletableEntityRepository<HospitalData> hospitalDataRepository,
            IDeletableEntityRepository<RecipientRequest> recipientRequesDataRepository)
        {
            this.requestsRepository = requestsRepository;
            this.requestsHospitalDataRepository = requestsHospitalDataRepository;
            this.hospitalDataRepository = hospitalDataRepository;
            this.recipientRequesDataRepository = recipientRequesDataRepository;
        }

        public async Task<string> CreateRequestAsync(string userId, RequestInputViewModel input)
        {
            var hospitalData = this.hospitalDataRepository.All()
                .Where(hd => hd.ApplicationUserId == userId)
                .FirstOrDefault();

            var request = new Request
            {
                HospitalName = hospitalData.Name,
                Content = input.Content,
                PublishedOn = input.PublishedOn,
                EmergencyStatus = input.EmergencyStatus,
                BloodGroup = input.BloodGroup,
                RhesusFactor = input.RhesusFactor,
                NeededQuantity = input.NeededQuantity,
            };

            request.HospitalId = hospitalData.Id;

            await this.requestsRepository.AddAsync(request);
            await this.requestsRepository.SaveChangesAsync();

            var userHospital = new HospitalDataRequest
            {
                HospitalDataId = hospitalData.Id,
                RequestId = request.Id,
            };

            await this.requestsHospitalDataRepository.AddAsync(userHospital);
            await this.requestsHospitalDataRepository.SaveChangesAsync();

            return request.Id;
        }

        public async Task DeleteAsync(string requestId)
        {
            var request = this.requestsRepository.All()
                .Where(r => r.Id == requestId)
                .FirstOrDefault();

            if (request == null)
            {
                throw new ArgumentNullException();
            }

            request.IsDeleted = true;
            await this.requestsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> AllRequests<T>()
        {
            var requests = this.requestsRepository.All()
                .Where(r => r.IsDeleted == false);

            return requests.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var request = this.requestsRepository.All()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefault();

            return request;
        }
    }
}
