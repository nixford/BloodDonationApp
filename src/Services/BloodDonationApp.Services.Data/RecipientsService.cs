﻿namespace BloodDonationApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;
    using BloodDonationApp.Web.ViewModels.Recipient;

    public class RecipientsService : IRecipientsService
    {
        private readonly IDeletableEntityRepository<Recipient> recipientsRepository;
        private readonly IDeletableEntityRepository<ApplicationUserHospitalData> hospitalDataUserRepository;
        private readonly IDeletableEntityRepository<RecipientRequest> recipientRequestDataRepository;
        private readonly IDeletableEntityRepository<RecipientHospitalData> recipientHospitalDataRepository;

        public RecipientsService(
            IDeletableEntityRepository<Recipient> recipientsRepository,
            IDeletableEntityRepository<ApplicationUserHospitalData> hospitalDataRepository,
            IDeletableEntityRepository<RecipientRequest> recipientRequestDataRepository,
            IDeletableEntityRepository<RecipientHospitalData> recipientHospitalDataRepository)
        {
            this.recipientsRepository = recipientsRepository;
            this.hospitalDataUserRepository = hospitalDataRepository;
            this.recipientRequestDataRepository = recipientRequestDataRepository;
            this.recipientHospitalDataRepository = recipientHospitalDataRepository;
        }

        public async Task AddRecipientAsync(string userHospitalId, RecipientInputModel input)
        {
            var hospitalData = this.hospitalDataUserRepository.All()
                .FirstOrDefault(uhd => uhd.ApplicationUserId == userHospitalId);

            var recipient = new Recipient
            {
                FirstName = input.FirstName,
                MiddleName = input.MiddleName,
                LastName = input.LastName,
                Age = input.Age,
                NeededQuantity = input.NeededQuantity,
                RecipientEmergency = input.RecipientEmergency,
                BloodGroup = input.BloodGroup,
                RhesusFactor = input.RhesusFactor,
            };

            var recipientHospitalData = new RecipientHospitalData
            {
                HospitalDataId = hospitalData.HospitalDataId,
                RecipientId = recipient.Id,
            };

            recipient.HospitalDataId = recipientHospitalData.HospitalDataId;

            await this.recipientsRepository.AddAsync(recipient);
            await this.recipientsRepository.SaveChangesAsync();

            await this.recipientHospitalDataRepository.AddAsync(recipientHospitalData);
            await this.recipientHospitalDataRepository.SaveChangesAsync();
        }

        public IEnumerable<T> AllHospitalRecipients<T>(string userHospitalId)
        {
            var recipientsCurrHospital = this.recipientsRepository.All()
                .Where(r => r.IsDeleted == false
                && r.HospitalData.ApplicationUserId == userHospitalId);

            return recipientsCurrHospital.To<T>().ToList();
        }

        public IEnumerable<T> TotalRecipients<T>()
        {
            var recipients = this.recipientsRepository.All()
                .Where(r => r.IsDeleted == false);

            return recipients.To<T>().ToList();
        }

        public async Task DeleteAsync(string userHospitalId, string recipientId)
        {
            var recipientsCurrHospital = this.recipientsRepository.All()
                .Where(r => r.IsDeleted == false &&
                r.HospitalData.ApplicationUserId == userHospitalId);

            var recipient = recipientsCurrHospital
                .FirstOrDefault(r => r.Id == recipientId);

            if (recipient == null)
            {
                throw new ArgumentNullException();
            }

            recipient.IsDeleted = true;
            await this.recipientsRepository.SaveChangesAsync();
        }

        public T GetById<T>(string recipientId)
        {
            var recipient = this.recipientsRepository.All()
                 .Where(r => r.Id == recipientId)
                 .To<T>()
                 .FirstOrDefault();

            return recipient;
        }

        public async Task AddRequestForRecipient(string requestId, string recipientId)
        {
            if (this.recipientRequestDataRepository.All().Any(x => x.RequestId == requestId && x.RecipientId == recipientId))
            {
                return;
            }

            var recipientRequest = new RecipientRequest
            {
                RecipientId = recipientId,
                RequestId = requestId,
            };

            await this.recipientRequestDataRepository.AddAsync(recipientRequest);
            await this.recipientRequestDataRepository.SaveChangesAsync();
        }
    }
}
