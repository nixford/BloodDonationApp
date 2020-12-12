namespace BloodDonationApp.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using Moq;
    using Xunit;

    public class DonorsServiceTest
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var list = new List<DonorData>();
            var mockRepo = new Mock<IRepository<DonorData>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            
            // var service = new DonorsService(mockRepo.Object, null, null, null);


            Assert.Equal(1, list.Count);

        }
    }
}
