namespace Scio.Web.Areas.Classroom.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Scio.Data.Models;
    using Scio.Data.Models.Enums;
    using Scio.Services.Data;
    using Scio.Web.ViewModels.Classroom.Courses;

    [Area("Classroom")]
    public class CoursesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICourseService courseService;

        public CoursesController(
            UserManager<ApplicationUser> userManager,
            ICourseService courseService)
        {
            this.userManager = userManager;
            this.courseService = courseService;
        }

        public IActionResult New() => this.View();

        [HttpGet]
        [AllowAnonymous]
        public IActionResult All()
        {
            var courses = this.courseService
                .GetAll<CourseViewModel>();

            return this.View(courses);
        }

        [HttpPost]
        public async Task<IActionResult> New(InputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
            var courseId = await this.courseService
                .CreateAsync(input.Title, input.Description, input.Type, userId);

            return this.RedirectToAction(nameof(this.Details), new { id = courseId });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            var course = this.courseService
                .Get<DetailsViewModel>(id);

            return this.View(course);
        }

        [HttpGet]
        public async Task<IActionResult> AddUser(string id) // Move to API
        {
            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            var course = this.courseService
                .SearchForUser<CourseUserValidationModel>(id, userId);

            if (course == null || course.ContainsUser)
            {
                return this.BadRequest();
            }

            int status = course.Type == CourseType.Public ? 1 : 0;

            var courseId = await this.courseService
                .AddUserAsync(id, userId, status);

            return this.RedirectToAction(nameof(this.Details), new { id });
        }
    }
}
