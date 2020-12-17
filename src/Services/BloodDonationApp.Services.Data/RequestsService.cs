namespace BloodDonationApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BloodDonationApp.Common;
    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
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

        public async Task<string> CreateRequestAsync(
            string userId,
            string content,
            DateTime publishedOn,
            EmergencyStatus emergencyStatus,
            BloodGroup bloodGroup,
            RhesusFactor rhesusFactor,
            double neededQuantity)
        {
            if (neededQuantity == 0)
            {
                throw new ArgumentException(GlobalConstants.NoQuantityErrrorMessage);
            }

            var hospitalData = this.hospitalDataRepository.All()
                .Where(hd => hd.ApplicationUserId == userId)
                .FirstOrDefault();

            if (hospitalData == null)
            {
                throw new ArgumentException(GlobalConstants.NoHospitalDataErrorMessage);
            }

            var location = this.locationDataRepository.All()
                .FirstOrDefault(l => l.Id == hospitalData.LocationId);

            var request = new Request
            {
                HospitalName = hospitalData.Name,
                Content = content,
                PublishedOn = publishedOn,
                EmergencyStatus = emergencyStatus,
                BloodGroup = bloodGroup,
                RhesusFactor = rhesusFactor,
                NeededQuantity = neededQuantity,
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

        public IEnumerable<T> AllRequests<T>(string? userId, int? take = null, int skip = 0)
        {
            var hospitaData = this.hospitalDataRepository
                .All()
                .FirstOrDefault(h => h.ApplicationUserId == userId);

            var requests = this.requestsRepository.All();

            if (hospitaData == null)
            {
                requests = this.requestsRepository.All()
                .OrderByDescending(r => r.CreatedOn)
                .Where(r => r.IsDeleted == false)
                .Skip(skip);
            }
            else
            {
                requests = this.requestsRepository.All()
                .OrderByDescending(r => r.CreatedOn)
                .Where(r => r.IsDeleted == false && r.HospitalId == hospitaData.Id)
                .Skip(skip);
            }

            if (take.HasValue)
            {
                requests = requests.Take(take.Value);
            }

            return requests.To<T>().ToList();
        }

        public async Task DeleteAsync(string requestId)
        {
            var request = this.requestsRepository.All()
                .Where(r => r.Id == requestId)
                .FirstOrDefault();

            if (request == null)
            {
                throw new ArgumentException(GlobalConstants.NoRequestErrorMessage);
            }

            request.IsDeleted = true;
            await this.requestsRepository.SaveChangesAsync();
        }

        public T GetById<T>(string id)
        {
            if (id == null)
            {
                throw new ArgumentException(GlobalConstants.NoRequestErrorMessage);
            }

            var request = this.requestsRepository.All()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefault();

            return request;
        }
    }
}
