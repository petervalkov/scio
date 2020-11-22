namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IResourceService
    {
        public Task<string> CreateAsync(string title, IFormFile file, string courseId, string authorId);
    }
}
