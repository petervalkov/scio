namespace Scio.Web.Areas.Classroom.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Scio.Services.Data;
    using Scio.Web.ViewModels.Classroom.Lectures;

    [Area("Classroom")]
    public class LecturesController : Controller
    {
        private readonly ILectureService lectureService;
        private readonly IResourceService resourceService;

        public LecturesController(
            ILectureService lectureService,
            IResourceService resourceService)
        {
            this.lectureService = lectureService;
            this.resourceService = resourceService;
        }

        [HttpPost]
        public async Task<IActionResult> New(InputModel input)
        {
            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            var lectureId = await this.lectureService
                .CreateAsync(input.Title, input.CourseId, input.Files, userId);

            if (lectureId != null)
            {
                await this.resourceService
                    .UploadAsync(input.Files, lectureId, input.CourseId, userId);

                return this.RedirectToAction("Settings", "Courses", new { id = input.CourseId });
            }

            return this.BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> UploadResourse(InputModel input)
        {
            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            var lectureId = await this.lectureService
                .CreateAsync(input.Title, input.CourseId, input.Files, userId);

            if (lectureId != null)
            {
                await this.resourceService
                    .UploadAsync(input.Files, lectureId, input.CourseId, userId);

                return this.RedirectToAction("Settings", "Courses", new { id = input.CourseId });
            }

            return this.BadRequest();
        }
    }
}
