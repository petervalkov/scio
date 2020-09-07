namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    public interface IAnswerService
    {
        Task<string> CreateAsync(string content, string questionId, string authorId);
    }
}
