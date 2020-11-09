namespace Scio.Web.Areas.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Scio.Common;
    using Scio.Data.Models.Enums;
    using Scio.Services.Data;
    using Scio.Web.Infrastructure.Validation;
    using Scio.Web.ViewModels.Classroom.Courses;

    [Route("api/[controller]")]
    [ApiController]
    public class CourseUsersController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CourseUsersController(
            ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Join(JoinRequestModel input)
        {
            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
            var course = this.courseService
                .GetValidationModel(input.CourseId, userId);

            if (course == null || course.UserStatus != null)
            {
                return this.BadRequest();
            }

            int status = course.Type switch
            {
                CourseType.Public => (int)CourseUserStatus.Accepted,
                CourseType.Private => (int)CourseUserStatus.Pending,
                _ => (int)CourseUserStatus.Pending,
            };

            var result = (CourseUserStatus)await this.courseService // Check if successfull
                .AddUserAsync(input.CourseId, userId, status);

            string message = result switch
            {
                CourseUserStatus.Accepted => Message.SuccessfullJoin,
                CourseUserStatus.Pending => Message.RequestSent,
                _ => Message.SuccessDefault,
            };

            return this.Ok(message);
        }

        [HttpPut]
        [ValidateModelState]
        public async Task<IActionResult> Update(UpdateUserStatusModel input)
        {
            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
            var course = this.courseService
                .GetValidationModel(input.CourseId, input.UserId);

            if (course == null || course.UserStatus == null || course.AuthorId != userId)
            {
                return this.BadRequest();
            }

            var result = (CourseUserStatus)await this.courseService
                .UpdateUserStatusAsync(input.CourseId, input.UserId, input.Status);

            return this.Ok(result.ToString());
        }
    }
}
