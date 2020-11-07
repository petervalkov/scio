namespace Scio.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;
    using Scio.Data.Models.Enums;
    using Scio.Services.Mapping;

    public class CourseService : ICourseService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly IDeletableEntityRepository<CourseUser> courseUserRepository;

        public CourseService(
            IDeletableEntityRepository<Course> courseRepository,
            IDeletableEntityRepository<CourseUser> courseUserRepository)
        {
            this.courseRepository = courseRepository;
            this.courseUserRepository = courseUserRepository;
        }

        public IEnumerable<TModel> GetAll<TModel>()
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

        public TModel SearchForUser<TModel>(string id, string userId)
        {
            return this.courseRepository
                .AllWithDeletedIncluding("Users")
                .Where(x => x.Id == id)
                .Select(x => new Course
                {
                    Id = x.Id,
                    Type = x.Type,
                    Users = x.Users.Where(u => u.UserId == userId).ToList(),
                })
                .To<TModel>()
                .FirstOrDefault();
        }

        public async Task<string> CreateAsync(string title, string description, int type, string authorId)
        {
            var course = new Course
            {
                Title = title,
                Description = description,
                Type = (CourseType)type,
                AuthorId = authorId,
            };

            await this.courseRepository.AddAsync(course);
            await this.courseRepository.SaveChangesAsync();

            return course.Id;
        }

        public async Task<string> AddUserAsync(string courseId, string userId, int status)
        {
            var courseUser = new CourseUser
            {
                CourseId = courseId,
                UserId = userId,
                Status = (CourseUserStatus)status,
            };

            await this.courseUserRepository.AddAsync(courseUser);
            await this.courseRepository.SaveChangesAsync();

            return "success";
        }
    }
}
