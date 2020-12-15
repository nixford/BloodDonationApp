namespace BloodDonationApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonationApp.Data;
    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Data.Repositories;
    using BloodDonationApp.Services.Data.Tests.InMemory;
    using BloodDonationApp.Web.ViewModels.Message;
    using Moq;
    using Xunit;

    public class MessagesServiceTests
    {
        [Fact]
        public async Task CreateMessageShoidReturnFiveTest()
        {
            var list = new List<Message>();
            var mockRepo = new Mock<IDeletableEntityRepository<Message>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Message>())).Callback(
                (Message message) => list.Add(message));
            var service = new MessagesService(mockRepo.Object);

            await service.Create("Hello1", "1", "user1", "user1@gmail.com");
            await service.Create("Hello2", "2", "user2", "user2@gmail.com");
            await service.Create("Hello3", "3", "user3", "user3@gmail.com");
            await service.Create("Hello4", "4", "user4", "user4@gmail.com");
            await service.Create("Hello5", "5", "user5", "user5@gmail.com");

            Assert.Equal(5, list.Count);
        }

        [Fact]
        public async Task CreateMessageInMemoryDbTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            await this.SeedDataAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Message>(dbContext);
            var service = new MessagesService(repository);

            await service.Create("Hello5", "5", "user5", "user5@gmail.com");

            Assert.Equal(2, repository.All().Count());
        }

        [Fact]
        public async Task CreateMessageThrowExceptionTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            await this.SeedDataAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Message>(dbContext);
            var service = new MessagesService(repository);

            await Assert.ThrowsAsync<ArgumentException>(() => service.Create(null, "5", "user5", "user5@gmail.com"));
        }

        [Fact]
        public async Task GetAllMessagesShouldReturnOneTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            MapperInitializer.InitializeMapper();

            await this.SeedDataAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Message>(dbContext);
            var service = new MessagesService(repository);

            var result = service.GetAllMessages<MessageViewModel>().Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteMesageRemovesCorrectlyMessageTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            await this.SeedDataAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Message>(dbContext);
            var service = new MessagesService(repository);
            var message = repository
                .All()
                .FirstOrDefault(m => m.AuthorId == "user1");

            await service.Delete(message.Id);

            Assert.Equal(0, repository.All().Count());
        }

        [Fact]
        public async Task DeleteMesageThrowsExceptionIfMessageIsNullTest()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            await this.SeedDataAsync(dbContext);

            var repository = new EfDeletableEntityRepository<Message>(dbContext);
            var service = new MessagesService(repository);

            await Assert.ThrowsAsync<ArgumentException>(() => service.Delete("111"));
        }

        private async Task SeedDataAsync(ApplicationDbContext dbContext)
        {
            dbContext.Messages.Add(new Message
            {
                Content = "Hello1",
                AuthorId = "user1",
                Email = "user1@gmail.com",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
