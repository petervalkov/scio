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

        public async Task UploadAsync(IFormFile[] files, string lectureId, string courseId, string authorId)
        {
            foreach (var file in files)
            {
                var resource = new Resource
                {
                    Title = file.FileName,
                    LectureId = lectureId,
                    CourseId = courseId,
                    AuthorId = authorId,
                };

                var resourceUrl = await this.SaveToFileSystem(resource.Id, courseId, file);
                resource.Url = resourceUrl;

                await this.resourceRepository.AddAsync(resource);
            }

            await this.resourceRepository.SaveChangesAsync();
        }

        private async Task<string> SaveToFileSystem(string resourceId, string courseId, IFormFile file) // Temporary
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
