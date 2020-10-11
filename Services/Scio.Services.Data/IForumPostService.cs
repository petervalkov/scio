namespace Scio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IForumPostService
    {
        TViewModel GetById<TViewModel>(string id);

        IEnumerable<TViewModel> GetAllQuestions<TViewModel>();

        Task<string> CreateQuestionAsync(string title, string body, string authorId);

        Task EditQuestionAsync(string id, string title, string body);

        Task CreateAnswerAsync(string body, string questionId, string authorId);

        Task EditAnswerAsync(string id, string body);

        Task DeleteAsync(string id);
    }
}
