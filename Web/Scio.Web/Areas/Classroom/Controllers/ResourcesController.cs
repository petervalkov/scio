namespace Scio.Web.Areas.Classroom.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Scio.Services.Data;
    using Scio.Web.ViewModels.Classroom.Resources;

    [Area("Classroom")]
    public class ResourcesController : Controller
    {
        private readonly IResourceService resourceService;

        public ResourcesController(IResourceService resourceService)
        {
            this.resourceService = resourceService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(InputModel input)
        {
            var userId = this.User
                  .FindFirstValue(ClaimTypes.NameIdentifier);

            var resourceId = await this.resourceService
                .CreateAsync(input.Title, input.Resource, input.CourseId, userId);

            if (resourceId == null)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("Settings", "Courses", new { id = input.CourseId });
        }
    }
}
