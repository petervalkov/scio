namespace Scio.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;
    using Scio.Data.Models.Enums;
    using Scio.Services.Mapping;

    public class CourseService : ICourseService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CourseService(IDeletableEntityRepository<Course> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public IEnumerable<TModel> All<TModel>()
            => this.courseRepository
            .AllAsNoTracking()
            .To<TModel>()
            .ToList();

        public TModel Get<TModel>(string id)
            => this.courseRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id)
            .To<TModel>()
            .SingleOrDefault();

        public async Task<string> CreateAsync(string title, string description, int type, string authorId)
        {
            var course = new Course
            {
                Title = title,
                Description = description,
                Type = (CourseType)type,
                AuthorId = authorId,
            };

            course.Users.Add(new CourseUser
            {
                CourseId = course.Id,
                UserId = authorId,
                Status = CourseUserStatus.Accepted,
                Role = CourseRole.Admin,
            });

            await this.courseRepository.AddAsync(course);
            await this.courseRepository.SaveChangesAsync();

            return course.Id;
        }
    }
}
