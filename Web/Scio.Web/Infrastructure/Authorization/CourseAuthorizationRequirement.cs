namespace Scio.Web.Infrastructure.Authorization
{
    using Microsoft.AspNetCore.Authorization;

    public class CourseAuthorizationRequirement : IAuthorizationRequirement
    {
        public CourseAuthorizationRequirement(string type)
        {
            this.Type = type;
        }

        public string Type { get; set; }
    }
}
