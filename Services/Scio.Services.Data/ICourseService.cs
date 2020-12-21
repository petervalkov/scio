namespace Scio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        IEnumerable<TModel> All<TModel>();

        TModel Get<TModel>(string id);

        Task<string> CreateAsync(string title, string description, int type, string authorId);

        Task<string> UpdateAsync(string id, string title, string description, int type);
    }
}
