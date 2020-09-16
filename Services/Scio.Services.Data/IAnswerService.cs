namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    public interface IAnswerService
    {
        Task CreateAsync(string content, string questionId, string authorId);

        TViewModel GetById<TViewModel>(string id);

        Task EditAsync(string id, string content);

        Task DeleteAsync(string id);
    }
}
