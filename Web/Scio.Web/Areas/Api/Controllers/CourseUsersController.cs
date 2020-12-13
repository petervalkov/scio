namespace Scio.Web.Areas.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Scio.Common;
    using Scio.Data.Models;
    using Scio.Services.Data;
    using Scio.Web.Helpers;
    using Scio.Web.Infrastructure.Filters;
    using Scio.Web.ViewModels.Classroom;
    using Scio.Web.ViewModels.Classroom.Courses;

    [Route("api/[controller]")]
    [ApiController]
    public class CourseUsersController : ControllerBase
    {
        private readonly ICourseUserService courseUserService;
        private readonly UserManager<ApplicationUser> userManager;

        public CourseUsersController(
            ICourseUserService courseUserService,
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.courseUserService = courseUserService;
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Join(JoinRequestModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var course = this.courseUserService.FindCourseUser(input.CourseId, userId);

            if (course == null || course.UserStatus != null)
            {
                return this.BadRequest();
            }

            var courseUserStatus = course.Type switch
            {
                1 => await this.courseUserService.CreateAsync(input.CourseId, userId, 0, 0),
                0 => await this.courseUserService.CreateAsync(input.CourseId, userId, 1, 1),
                _ => 0
            };

            if (courseUserStatus == 1)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                await this.userManager.AddClaimAsync(user, new Claim(CourseRoleName.Member, course.Id));
            }

            string message = courseUserStatus switch
            {
                1 => Message.SuccessfullJoin,
                0 => Message.RequestSent,
                _ => Message.SuccessDefault,
            };

            return this.Ok(message);
        }

        [HttpPut]
        [Authorize(Policy = CourseRoleName.Admin)]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Update(UpdateUserStatusModel input)
        {
            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
            var course = this.courseUserService
                .FindCourseUser(input.CourseId, input.UserId);

            if (course == null || course.UserStatus == null || course.UserStatus == input.Status)
            {
                return this.BadRequest();
            }

            var result = await this.courseUserService
                .UpdateStatusAsync<CourseUserModel>(input.CourseId, input.UserId, input.Status);

            if (result == null)
            {
                return this.StatusCode(500);
            }

            var user = await this.userManager.FindByIdAsync(input.UserId);

            if (result.Status == "Accepted")
            {
                await this.userManager.AddClaimAsync(user, new Claim(CourseRoleName.Member, course.Id));
            }
            else
            {
                var claim = user.Claims.FirstOrDefault(x => x.ClaimType == CourseRoleName.Member && x.ClaimValue == course.Id);
                user.Claims.Remove(claim);
            }

            result.Date = TimeSpanParser.Parse((DateTime)(result.ModifiedOn ?? result.CreatedOn));

            return this.Ok(result);
        }
    }
}
