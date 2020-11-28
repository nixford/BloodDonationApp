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
        private readonly IDeletableEntityRepository<Location> locationDataRepository;

        public RequestsService(
            IDeletableEntityRepository<Request> requestsRepository,
            IDeletableEntityRepository<HospitalDataRequest> requestsHospitalDataRepository,
            IDeletableEntityRepository<HospitalData> hospitalDataRepository,
            IDeletableEntityRepository<RecipientRequest> recipientRequesDataRepository, 
            IDeletableEntityRepository<Location> locationDataRepository)
        {
            this.requestsRepository = requestsRepository;
            this.requestsHospitalDataRepository = requestsHospitalDataRepository;
            this.hospitalDataRepository = hospitalDataRepository;
            this.recipientRequesDataRepository = recipientRequesDataRepository;
            this.locationDataRepository = locationDataRepository;
        }

        public async Task<string> CreateRequestAsync(string userId, RequestInputViewModel input)
        {
            var hospitalData = this.hospitalDataRepository.All()
                .Where(hd => hd.ApplicationUserId == userId)
                .FirstOrDefault();

            var location = this.locationDataRepository.All()
                .FirstOrDefault(l => l.Id == hospitalData.LocationId);

            var request = new Request
            {
                HospitalName = hospitalData.Name,
                Content = input.Content,
                PublishedOn = input.PublishedOn,
                EmergencyStatus = input.EmergencyStatus,
                BloodGroup = input.BloodGroup,
                RhesusFactor = input.RhesusFactor,
                NeededQuantity = input.NeededQuantity,
                Location = location,
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

        public IEnumerable<T> AllRequests<T>(int? take = null, int skip = 0)
        {
            var requests = this.requestsRepository.All()
                .OrderByDescending(r => r.CreatedOn)
                .Where(r => r.IsDeleted == false)
                .Skip(skip);

            return requests.To<T>().ToList();
        }

        public IEnumerable<Request> AllRequestsCount()
        {
            var requests = this.requestsRepository
                .All()
                .Where(r => r.IsDeleted == false)
                .ToList();

            return requests;
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
