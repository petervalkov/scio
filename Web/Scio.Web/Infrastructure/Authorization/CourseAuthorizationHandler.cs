namespace Scio.Web.Infrastructure.Authorization
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Scio.Common;
    using Scio.Services.Data;

    public class CourseAuthorizationHandler : AuthorizationHandler<CourseAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ICourseUserService courseUserService;

        public CourseAuthorizationHandler(
            IHttpContextAccessor contextAccessor,
            ICourseUserService courseUserService)
        {
            this.contextAccessor = contextAccessor;
            this.courseUserService = courseUserService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CourseAuthorizationRequirement requirement)
        {
            var request = this.contextAccessor.HttpContext.Request;
            string courseId = request.Method switch
            {
                "GET" => request.RouteValues["id"]?.ToString(),
                "POST" => request.Form["courseId"],
                _ => null,
            };

            if (courseId != null)
            {
                if (requirement.Type == CourseRoleName.Member)
                {
                    this.AuthorizeMember(courseId, context, requirement);
                }
                else if (requirement.Type == CourseRoleName.Admin)
                {
                    this.AuthorizeAdmin(courseId, context, requirement);
                }
            }

            return Task.CompletedTask;
        }

        private void AuthorizeMember(string courseId, AuthorizationHandlerContext context, CourseAuthorizationRequirement requirement)
        {
            // FIX
            if (context.User.HasClaim(CourseRoleName.Member, courseId) || context.User.HasClaim(CourseRoleName.Admin, courseId))
            {
                context.Succeed(requirement);
                return;
            }

            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var courseUser = this.courseUserService.FindCourseUser(courseId, userId);

            if (courseUser.UserRole == 1 || courseUser.UserRole == 2)
            {
                context.Succeed(requirement);
            }
        }

        private void AuthorizeAdmin(string courseId, AuthorizationHandlerContext context, CourseAuthorizationRequirement requirement)
        {
            if (context.User.HasClaim(CourseRoleName.Admin, courseId))
            {
                context.Succeed(requirement);
                return;
            }

            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var courseUser = this.courseUserService.FindCourseUser(courseId, userId);

            if (courseUser.UserRole == 2)
            {
                context.Succeed(requirement);
            }
        }
    }
}
