namespace BloodDonationApp.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Models.Enums;
    using BloodDonationApp.Data.Repositories;
    using BloodDonationApp.Services.Data.Tests.InMemory;
    using BloodDonationApp.Web.ViewModels.Request;
    using Xunit;

    public class RequestsServiceTests
    {
        [Fact]
        public void AllRequestsShouldReturnNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var hospitalDataRequestRepository = new EfDeletableEntityRepository<HospitalDataRequest>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var locationRepository = new EfDeletableEntityRepository<Location>(dbContext);

            var service = new RequestsService(
                requestRepository,
                hospitalDataRequestRepository,
                hospitalDataRepository,
                recipientReques,
                locationRepository);

            var result = service.AllRequests<RequestInfoViewModel>(null).Count();

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task AllRequestsShouldReturnNotNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var hospitalDataRequestRepository = new EfDeletableEntityRepository<HospitalDataRequest>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var locationRepository = new EfDeletableEntityRepository<Location>(dbContext);

            var service = new RequestsService(
                requestRepository,
                hospitalDataRequestRepository,
                hospitalDataRepository,
                recipientReques,
                locationRepository);

            await SeedDataAsync(dbContext);

            var result = service.AllRequests<RequestInfoViewModel>(null).Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task CreateRequestNotNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var hospitalDataRequestRepository = new EfDeletableEntityRepository<HospitalDataRequest>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var locationRepository = new EfDeletableEntityRepository<Location>(dbContext);

            var service = new RequestsService(
                requestRepository,
                hospitalDataRequestRepository,
                hospitalDataRepository,
                recipientReques,
                locationRepository);

            await SeedDataAsync(dbContext);

            var result = requestRepository.All().FirstOrDefault();

            Assert.NotNull(result);
        }

        [Fact]
        public void CreateRequestNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var hospitalDataRequestRepository = new EfDeletableEntityRepository<HospitalDataRequest>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var locationRepository = new EfDeletableEntityRepository<Location>(dbContext);

            var service = new RequestsService(
                requestRepository,
                hospitalDataRequestRepository,
                hospitalDataRepository,
                recipientReques,
                locationRepository);

            var result = requestRepository.All().FirstOrDefault();

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateRequestCorrectHospitalDataRequestEntityTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var hospitalDataRequestRepository = new EfDeletableEntityRepository<HospitalDataRequest>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var locationRepository = new EfDeletableEntityRepository<Location>(dbContext);

            var service = new RequestsService(
                requestRepository,
                hospitalDataRequestRepository,
                hospitalDataRepository,
                recipientReques,
                locationRepository);

            await SeedDataAsync(dbContext);

            var request = requestRepository
                    .All()
                    .Where(r => r.NeededQuantity == 500)
                    .FirstOrDefault();

            var requestHospitalData = hospitalDataRequestRepository
                    .All()
                    .FirstOrDefault();

            Assert.Equal(request.Id, requestHospitalData.RequestId);
        }

        [Fact]
        public async Task CreateRequestShouldReturnNotNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var hospitalDataRequestRepository = new EfDeletableEntityRepository<HospitalDataRequest>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var locationRepository = new EfDeletableEntityRepository<Location>(dbContext);

            var service = new RequestsService(
                requestRepository,
                hospitalDataRequestRepository,
                hospitalDataRepository,
                recipientReques,
                locationRepository);

            // Seeding user
            var user = new ApplicationUser
            {
                UserName = "User1",
                Email = "User1@user.bg",
            };
            dbContext.Users.Add(user);

            // Seeding hospital
            var hospital = new HospitalData
            {
                ApplicationUserId = user.Id,
                Name = "HospitalName",
                Contact = new Contact
                {
                    Phone = "123456789",
                    Email = "hospital@hospital.bg",
                },
                Location = new Location
                {
                    Country = "Bulgaria",
                    City = "Sofia",
                    AdressDescription = "Sofia",
                },
            };
            dbContext.HospitalData.Add(hospital);

            await dbContext.SaveChangesAsync();

            await service.CreateRequestAsync(hospital.ApplicationUserId, "...", DateTime.UtcNow, EmergencyStatus.WithinDay, BloodGroup.A, RhesusFactor.Negative, 500);

            var result = requestRepository
                    .All()
                    .Where(r => r.NeededQuantity == 500)
                    .FirstOrDefault();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateRequestShouldthrowExceptionDueToNullHospitalDataTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var hospitalDataRequestRepository = new EfDeletableEntityRepository<HospitalDataRequest>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var locationRepository = new EfDeletableEntityRepository<Location>(dbContext);

            var service = new RequestsService(
                requestRepository,
                hospitalDataRequestRepository,
                hospitalDataRepository,
                recipientReques,
                locationRepository);

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateRequestAsync("1", "...", DateTime.UtcNow, EmergencyStatus.WithinDay, BloodGroup.A, RhesusFactor.Negative, 500));
        }

        [Fact]
        public async Task CreateRequestShouldThrowExceptionIfInvalidDataIsPassTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var hospitalDataRequestRepository = new EfDeletableEntityRepository<HospitalDataRequest>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var locationRepository = new EfDeletableEntityRepository<Location>(dbContext);

            var service = new RequestsService(
                requestRepository,
                hospitalDataRequestRepository,
                hospitalDataRepository,
                recipientReques,
                locationRepository);

            // Seeding user
            var user = new ApplicationUser
            {
                UserName = "User1",
                Email = "User1@user.bg",
            };
            dbContext.Users.Add(user);

            // Seeding hospital
            var hospital = new HospitalData
            {
                ApplicationUserId = user.Id,
                Name = "HospitalName",
                Contact = new Contact
                {
                    Phone = "123456789",
                    Email = "hospital@hospital.bg",
                },
                Location = new Location
                {
                    Country = "Bulgaria",
                    City = "Sofia",
                    AdressDescription = "Sofia",
                },
            };
            dbContext.HospitalData.Add(hospital);

            await dbContext.SaveChangesAsync();

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateRequestAsync(hospital.ApplicationUserId, "content...", DateTime.UtcNow, EmergencyStatus.WithinDay, BloodGroup.A, RhesusFactor.Negative, 0));
        }

        [Fact]
        public async Task DeleteAsyncRequestThrowsExceptionTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var hospitalDataRequestRepository = new EfDeletableEntityRepository<HospitalDataRequest>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var locationRepository = new EfDeletableEntityRepository<Location>(dbContext);

            var service = new RequestsService(
                requestRepository,
                hospitalDataRequestRepository,
                hospitalDataRepository,
                recipientReques,
                locationRepository);

            await SeedDataAsync(dbContext);

            var request = requestRepository.All().FirstOrDefault();

            await service.DeleteAsync(request.Id);

            await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteAsync(request.Id));
        }

        [Fact]
        public async Task DeleteAsyncRequestCorrectlyTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var hospitalDataRequestRepository = new EfDeletableEntityRepository<HospitalDataRequest>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var locationRepository = new EfDeletableEntityRepository<Location>(dbContext);

            var service = new RequestsService(
                requestRepository,
                hospitalDataRequestRepository,
                hospitalDataRepository,
                recipientReques,
                locationRepository);

            await SeedDataAsync(dbContext);

            var request = requestRepository.All().FirstOrDefault();

            await service.DeleteAsync(request.Id);

            var result = service.AllRequests<RequestInfoViewModel>(null).Count();

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetRequestByIdShouldReturnCorrectlyTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var hospitalDataRequestRepository = new EfDeletableEntityRepository<HospitalDataRequest>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var locationRepository = new EfDeletableEntityRepository<Location>(dbContext);

            var service = new RequestsService(
                requestRepository,
                hospitalDataRequestRepository,
                hospitalDataRepository,
                recipientReques,
                locationRepository);

            await SeedDataAsync(dbContext);

            var request = requestRepository.All().FirstOrDefault();

            var result = service.GetById<RequestInfoViewModel>(request.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetRequestByIdShouldThrowErrorTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            var requestRepository = new EfDeletableEntityRepository<Request>(dbContext);
            var hospitalDataRequestRepository = new EfDeletableEntityRepository<HospitalDataRequest>(dbContext);
            var hospitalDataRepository = new EfDeletableEntityRepository<HospitalData>(dbContext);
            var recipientReques = new EfDeletableEntityRepository<RecipientRequest>(dbContext);
            var locationRepository = new EfDeletableEntityRepository<Location>(dbContext);

            var service = new RequestsService(
                requestRepository,
                hospitalDataRequestRepository,
                hospitalDataRepository,
                recipientReques,
                locationRepository);

            Assert.Throws<ArgumentException>(() => service.GetById<RequestInfoViewModel>(null));
        }

        private static async Task SeedDataAsync(ApplicationDbContext dbContext)
        {
            // Seeding user
            var user = new ApplicationUser
            {
                UserName = "User1",
                Email = "User1@user.bg",
            };
            dbContext.Users.Add(user);

            // Seeding hospital
            var hospital = new HospitalData
            {
                ApplicationUserId = user.Id,
                Name = "HospitalName",
                Contact = new Contact
                {
                    Phone = "123456789",
                    Email = "hospital@hospital.bg",
                },
                Location = new Location
                {
                    Country = "Bulgaria",
                    City = "Sofia",
                    AdressDescription = "Sofia",
                },
            };
            dbContext.HospitalData.Add(hospital);

            // Seeding request
            var request = new Request
            {
                HospitalName = hospital.Name,
                Content = "",
                PublishedOn = DateTime.UtcNow,
                EmergencyStatus = EmergencyStatus.WithinWeek,
                BloodGroup = BloodGroup.A,
                RhesusFactor = RhesusFactor.Negative,
                NeededQuantity = 500,
            };
            dbContext.Requests.Add(request);

            // Seeding recipientHospital
            dbContext.HospitalDonationRequests.Add(new HospitalDataRequest
            {
                HospitalDataId = hospital.Id,
                RequestId = request.Id,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
