﻿namespace BloodDonationApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;
    using BloodDonationApp.Web.ViewModels.Request;
    using Microsoft.AspNetCore.Authorization;

    public class RequestsService : IRequestsService
    {
        private readonly IDeletableEntityRepository<Request> requestsRepository;
        private readonly IDeletableEntityRepository<HospitalDataRequest> requestsHospitalDataRepository;
        private readonly IDeletableEntityRepository<HospitalData> hospitalDataRepository;

        public RequestsService(
            IDeletableEntityRepository<Request> requestsRepository,
            IDeletableEntityRepository<HospitalDataRequest> requestsHospitalDataRepository,
            IDeletableEntityRepository<HospitalData> hospitalDataRepository = null)
        {
            this.requestsRepository = requestsRepository;
            this.requestsHospitalDataRepository = requestsHospitalDataRepository;
            this.hospitalDataRepository = hospitalDataRepository;
        }

        public async Task<string> CreateRequestAsync(string userId, RequestInputViewModel input)
        {
            var hospitalData = this.hospitalDataRepository.All()
                .Where(hd => hd.ApplicationUserId == userId)
                .FirstOrDefault();

            var request = new Request
            {
                Content = input.Content,
                PublishedOn = input.PublishedOn,
                EmergencyStatus = input.EmergencyStatus,
                BloodType = new BloodType
                {
                    BloodGroup = input.BloodGroup,
                    RhesusFactor = input.RhesusFactor,
                },
                NeededQuantity = input.NeededQuantity,
            };

            request.HospitalId = hospitalData.ApplicationUserId;

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

        public async Task DeleteAsync(string hospitalId)
        {
            var request = this.requestsRepository.All()
                .Where(r => r.HospitalId == hospitalId)
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
