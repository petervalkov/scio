namespace Scio.Web.Areas.Classroom.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Scio.Services.Data;
    using Scio.Web.ViewModels.Classroom.Resourses;

    [Area("Classroom")]
    public class ResourseController : Controller
    {
        private readonly IResourceService resourceService;

        public ResourseController(IResourceService resourceService)
        {
            this.resourceService = resourceService;
        }

        public async Task<IActionResult> Upload(InputModel input)
        {
            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            await this.resourceService
                .UploadAsync(input.Files, input.LectureId, input.CourseId, userId);

            return this.RedirectToAction("Settings", "Courses", new { id = input.CourseId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(DeleteInputModel input)
        {
            var courseId = await this.resourceService.DeleteAsync(input.Id);

            return this.RedirectToAction("Settings", "Courses", new { id = courseId });
        }
    }
}
