namespace Scio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Scio.Services.Data.DTOs;

    public interface ICourseService
    {
        IEnumerable<TModel> All<TModel>();

        TModel Get<TModel>(string id);

        CourseValidationModel GetValidationModel(string id, string userId);

        Task<string> CreateAsync(string title, string description, int type, string authorId);

        Task<int> AddUserAsync(string courseId, string userId, int status);

        Task<int> UpdateUserStatusAsync(string courseId, string userId, int status);
    }
}
