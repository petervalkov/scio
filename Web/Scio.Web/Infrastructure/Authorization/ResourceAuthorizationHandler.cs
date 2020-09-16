namespace Scio.Web.Infrastructure.Authorization
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Scio.Common;
    using Scio.Data.Common.Models;
    using Scio.Data.Models;

    public class ResourceAuthorizationHandler : AuthorizationHandler<ResourceAuthorizationRequirement, IOwnable<string>>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ResourceAuthorizationHandler(
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(
                AuthorizationHandlerContext context,
                ResourceAuthorizationRequirement requirement,
                IOwnable<string> resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (resource.AuthorId == this.userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
