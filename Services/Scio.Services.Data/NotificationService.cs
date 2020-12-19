namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;
    using Scio.Data.Models.Enums;

    public class NotificationService : INotificationService
    {
        private readonly IDeletableEntityRepository<Notification> notificationRepository;

        public NotificationService(IDeletableEntityRepository<Notification> notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        public async Task<string> CreateAsync(string text, string link, int type, string recipientId, string authorId, string courseId)
        {
            var notification = new Notification
            {
                Text = text,
                Link = link,
                Type = (NotificationType)type,
                RecipientId = recipientId,
                AuthorId = authorId,
                CourseId = courseId,
            };

            await this.notificationRepository.AddAsync(notification);
            await this.notificationRepository.SaveChangesAsync();

            return notification.Id;
        }
    }
}
