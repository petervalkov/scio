namespace Scio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IQuestionService
    {
        Task<string> CreateAsync(string title, string content, string authorId);

        IEnumerable<TViewModel> GetAll<TViewModel>();

        TViewModel GetById<TViewModel>(string id);

        Task EditAsync(string id, string title, string content);

        Task DeleteAsync(string id);
    }
}
