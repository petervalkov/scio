namespace Scio.Services.Data
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;

    public class ResourceService : IResourceService
    {
        private readonly IDeletableEntityRepository<Resource> resourceRepository;

        public ResourceService(
            IDeletableEntityRepository<Resource> resourceRepository)
        {
            this.resourceRepository = resourceRepository;
        }

        public async Task<string> CreateAsync(string title, IFormFile file, string courseId, string authorId)
        {
            var resource = new Resource
            {
                Title = title,
                CourseId = courseId,
                AuthorId = authorId,
            };

            var resourceUrl = await this.UploadAsync(resource.Id, courseId, file);
            resource.Url = resourceUrl;

            await this.resourceRepository.AddAsync(resource);
            await this.resourceRepository.SaveChangesAsync();

            return resource.Id;
        }

        private async Task<string> UploadAsync(string resourceId, string courseId, IFormFile file) // Temporary
        {
            var fileName = resourceId + Path.GetExtension(file.FileName);
            var path = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "resources", courseId));

            using (var fileStream = new FileStream(Path.Combine(path.FullName, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Path.Combine("resources", courseId, fileName);
        }
    }
}
