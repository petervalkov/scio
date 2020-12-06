namespace Scio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IForumPostService
    {
        IEnumerable<TModel> Get<TModel>();

        TModel Get<TModel>(string id, string authorId = null);

        TModel Find<TModel>(string id, string authorId = null, string questionId = null);

        Task<string> CreateAsync(string title, string body, string questionId, string authorId);

        Task EditAsync(string id, string title, string body);

        Task DeleteAsync(string id);
    }
}
