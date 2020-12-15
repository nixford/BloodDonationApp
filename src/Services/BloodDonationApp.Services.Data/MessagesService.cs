namespace BloodDonationApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BloodDonationApp.Common;
    using BloodDonationApp.Data.Common.Repositories;
    using BloodDonationApp.Data.Models;
    using BloodDonationApp.Services.Mapping;
    using BloodDonationApp.Web.ViewModels.Message;

    public class MessagesService : IMessagesService
    {
        private readonly IDeletableEntityRepository<Message> messagesRepository;

        public MessagesService(IDeletableEntityRepository<Message> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }

        public async Task Create(string content, string authorId, string userName, string email)
        {
            if (content == null ||
                authorId == null ||
                userName == null ||
                email == null)
            {
                throw new ArgumentException(GlobalConstants.MissingMessageElementErrorMessage);
            }

            var message = new Message
            {
                UserName = userName,
                Email = email,
                Content = content,
                AuthorId = authorId,
            };

            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllMessages<T>(int? take = null, int skip = 0)
        {
            var message = this.messagesRepository
                .All()
                .OrderByDescending(m => m.CreatedOn)
                .Where(m => m.IsDeleted == false)
                .Skip(skip);

            if (take.HasValue)
            {
                message = message.Take(take.Value);
            }

            return message.To<T>().ToList();
        }

        public async Task<string> Delete(string id)
        {
            var messageToDelete = this.messagesRepository
                    .All()
                    .Where(m => m.Id == id)
                    .FirstOrDefault();

            if (messageToDelete == null)
            {
                throw new ArgumentException(GlobalConstants.NotMessageErrorMessage);
            }

            messageToDelete.IsDeleted = true;

            await this.messagesRepository.SaveChangesAsync();

            return messageToDelete.Id;
        }
    }
}
