namespace Scio.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;

    public class LectureService : ILectureService
    {
        private readonly IDeletableEntityRepository<Lecture> lecturesRepository;

        public LectureService(IDeletableEntityRepository<Lecture> lecturesRepository)
        {
            this.lecturesRepository = lecturesRepository;
        }

        public async Task<string> CreateAsync(string title, string courseId, IFormFile[] files, string authorId)
        {
            var lecture = new Lecture
            {
                Title = title,
                CourseId = courseId,
                AuthorId = authorId,
            };

            await this.lecturesRepository.AddAsync(lecture);
            await this.lecturesRepository.SaveChangesAsync();

            return lecture.Id;
        }
    }
}
