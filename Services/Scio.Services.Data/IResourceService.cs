namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IResourceService
    {
        Task UploadAsync(IFormFile[] files, string lectureId, string courseId, string authorId);

        Task<string> DeleteAsync(string id);
    }
}
