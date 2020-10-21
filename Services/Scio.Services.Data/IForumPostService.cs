namespace Scio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IForumPostService
    {
        TViewModel GetById<TViewModel>(string id);

        IEnumerable<TViewModel> GetAllQuestions<TViewModel>();

        Task<string> CreateAsync(string title, string body, string questionId, string authorId);

        Task EditAsync(string id, string title, string body);

        Task DeleteAsync(string id);

        TValidationModel SearchForVote<TValidationModel>(string postId, string userId);
    }
}
