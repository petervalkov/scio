namespace Scio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        IEnumerable<TModel> GetAll<TModel>();

        TModel Get<TModel>(string id);

        Task<string> CreateAsync(string title, string description, int type, string authorId);

        Task<string> AddUserAsync(string courseId, string userId, int status);

        TModel SearchForUser<TModel>(string id, string userId);
    }
}
