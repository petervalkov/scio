namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    using Scio.Services.Data.DTOs;

    public interface ICourseUserService
    {
        Task<int> CreateAsync(string courseId, string userId, int status, int role);

        Task<TModel> UpdateStatusAsync<TModel>(string courseId, string userId, int status);

        CourseValidationModel FindCourseUser(string id, string userId);
    }
}
