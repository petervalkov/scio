namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ILectureService
    {
        Task<string> CreateAsync(string title, string courseId, IFormFile[] file, string authorId);
    }
}
