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

    public class RecipientsService : IRecipientsService
    {
        private readonly IDeletableEntityRepository<Recipient> recipientsRepository;
        private readonly IDeletableEntityRepository<HospitalData> hospitalDataRepository;
        private readonly IDeletableEntityRepository<RecipientRequest> recipientRequestDataRepository;
        private readonly IDeletableEntityRepository<RecipientHospitalData> recipientHospitalDataRepository;

        public RecipientsService(
            IDeletableEntityRepository<Recipient> recipientsRepository,
            IDeletableEntityRepository<HospitalData> hospitalDataRepository,
            IDeletableEntityRepository<RecipientRequest> recipientRequestDataRepository,
            IDeletableEntityRepository<RecipientHospitalData> recipientHospitalDataRepository)
        {
            this.recipientsRepository = recipientsRepository;
            this.hospitalDataRepository = hospitalDataRepository;
            this.recipientRequestDataRepository = recipientRequestDataRepository;
            this.recipientHospitalDataRepository = recipientHospitalDataRepository;
        }

        public async Task AddRecipientAsync(
            string userHospitalId,
            string firstName,
            string middleName,
            string lastName,
            int age,
            double neededQuantity,
            EmergencyStatus recipientEmergency,
            BloodGroup bloodGroup,
            RhesusFactor rhesusFactor)
        {
            var hospitalData = this.hospitalDataRepository.All()
                .FirstOrDefault(uhd => uhd.ApplicationUserId == userHospitalId);

            if (hospitalData == null)
            {
                throw new ArgumentException(GlobalConstants.NoHospitalDataErrorMessage);
            }

            if (firstName == null ||
                middleName == null ||
                lastName == null ||
                age == 0 ||
                neededQuantity == 0)
            {
                throw new ArgumentException(GlobalConstants.NotFullRecipientDataErrorMessage);
            }

            var recipient = new Recipient
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Age = age,
                NeededQuantity = neededQuantity,
                RecipientEmergency = recipientEmergency,
                BloodGroup = bloodGroup,
                RhesusFactor = rhesusFactor,
            };

            var recipientHospitalData = new RecipientHospitalData
            {
                HospitalDataId = hospitalData.Id,
                RecipientId = recipient.Id,
            };

            recipient.HospitalDataId = recipientHospitalData.HospitalDataId;

            await this.recipientsRepository.AddAsync(recipient);
            await this.recipientsRepository.SaveChangesAsync();

            await this.recipientHospitalDataRepository.AddAsync(recipientHospitalData);
            await this.recipientHospitalDataRepository.SaveChangesAsync();
        }

        public IEnumerable<T> AllHospitalRecipients<T>(string userHospitalId, int? take = null, int skip = 0)
        {
            var recipientsCurrHospital = this.recipientsRepository
                .All()
                .OrderByDescending(rh => rh.CreatedOn)
                .Where(r => r.IsDeleted == false && r.HospitalData.ApplicationUserId == userHospitalId)
                .Skip(skip);

            if (take.HasValue)
            {
                recipientsCurrHospital = recipientsCurrHospital.Take(take.Value);
            }

            return recipientsCurrHospital.To<T>().ToList();
        }

        public IEnumerable<T> TotalRecipients<T>(string userHospitalId)
        {
            var recipients = this.recipientsRepository
                .All()
                .Where(r => r.IsDeleted == false && r.HospitalData.ApplicationUserId == userHospitalId)
                .To<T>()
                .ToList();

            return recipients;
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
                throw new ArgumentException(GlobalConstants.NotRecipientDataErrorMessage);
            }

            recipient.IsDeleted = true;
            await this.recipientsRepository.SaveChangesAsync();
        }

        public T GetById<T>(string recipientId)
        {
            if (recipientId == null)
            {
                throw new ArgumentException(GlobalConstants.RecipentIdConnotBeNullErrorMessage);
            }

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
                throw new ArgumentException(GlobalConstants.ExistingRequestForThisRecipient);
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
