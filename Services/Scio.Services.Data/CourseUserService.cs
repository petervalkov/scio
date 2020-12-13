namespace Scio.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Scio.Data.Common.Repositories;
    using Scio.Data.Models;
    using Scio.Data.Models.Enums;
    using Scio.Services.Data.DTOs;
    using Scio.Services.Mapping;

    public class CourseUserService : ICourseUserService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly IDeletableEntityRepository<CourseUser> courseUserRepository;

        public CourseUserService(
            IDeletableEntityRepository<Course> courseRepository,
            IDeletableEntityRepository<CourseUser> courseUserRepository)
        {
            this.courseRepository = courseRepository;
            this.courseUserRepository = courseUserRepository;
        }

        public CourseValidationModel FindCourseUser(string id, string userId)
            => this.courseRepository
            .AllWithDeletedIncluding("Users")
            .Where(x => x.Id == id)
            .Select(x => new CourseValidationModel
            {
                Id = x.Id,
                Type = (int)x.Type,
                UserStatus = x.Users
                    .Where(x => x.UserId == userId)
                    .Select(x => (int)x.Status)
                    .SingleOrDefault(),
                UserRole = x.Users
                    .Where(x => x.UserId == userId)
                    .Select(x => (int)x.Role)
                    .SingleOrDefault(),
            })
            .FirstOrDefault();

        public async Task<int> CreateAsync(string courseId, string userId, int status, int role)
        {
            var courseUser = new CourseUser
            {
                CourseId = courseId,
                UserId = userId,
                Status = (CourseUserStatus)status,
                Role = (CourseRole)role,
            };

            await this.courseUserRepository.AddAsync(courseUser);
            await this.courseRepository.SaveChangesAsync();

            return (int)courseUser.Status;
        }

        public async Task<TModel> UpdateStatusAsync<TModel>(string courseId, string userId, int status)
        {
            var courseUser = await this.courseUserRepository
                .FindByIdAsync(courseId, userId);

            courseUser.Status = (CourseUserStatus)status;
            courseUser.Role = courseUser.Status == CourseUserStatus.Accepted ? CourseRole.Member : CourseRole.None;
            await this.courseUserRepository.SaveChangesAsync();

            return AutoMapperConfig.MapperInstance.Map<TModel>(courseUser);
        }
    }
}
