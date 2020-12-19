namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    public interface INotificationService
    {
        Task<string> CreateAsync(string text, string link, int type, string recipientId, string authorId, string courseId);
    }
}
