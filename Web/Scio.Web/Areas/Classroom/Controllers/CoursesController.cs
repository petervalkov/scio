namespace Scio.Web.Areas.Classroom.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Scio.Common;
    using Scio.Data.Models;
    using Scio.Services.Data;
    using Scio.Web.ViewModels.Classroom.Courses;

    [Area("Classroom")]
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ICourseUserService courseUserService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAuthorizationService authorizationService;

        public CoursesController(
            ICourseService courseService,
            ICourseUserService courseUserService,
            UserManager<ApplicationUser> userManager,
            IAuthorizationService authorizationService)
        {
            this.courseService = courseService;
            this.courseUserService = courseUserService;
            this.userManager = userManager;
            this.authorizationService = authorizationService;
        }

        public IActionResult New() => this.View();

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Home()
        {
            var courses = this.courseService
                .All<HomeViewModel>();

            return this.View(courses);
        }

        [HttpPost]
        public async Task<IActionResult> New(InputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var courseId = await this.courseService.CreateAsync(input.Title, input.Description, input.Type, user.Id);

            if (courseId == null)
            {
                return this.StatusCode(500);
            }

            await this.userManager.AddClaimAsync(user, new Claim(CourseRoleName.Admin, courseId));
            return this.RedirectToAction(nameof(this.Details), new { id = courseId });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            DetailsViewModel course;
            var authorization = await this.authorizationService
                .AuthorizeAsync(this.User, CourseRoleName.Member);

            if (authorization.Succeeded)
            {
                course = this.courseService
                    .Get<DetailsViewModel>(id);
                this.ViewBag.IsMember = true;
            }
            else
            {
                course = this.courseService
                    .Get<MemberDetailsViewModel>(id);

                if (this.User != null)
                {
                    var courseUser = this.courseUserService
                        .FindCourseUser(id, this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    this.ViewBag.IsPending = courseUser.UserStatus;
                }

                this.ViewBag.IsMember = false;
            }

            return this.View(course);
        }

        [HttpGet]
        [Authorize(Policy = CourseRoleName.Admin)]
        public IActionResult Settings(string id)
        {
            var course = this.courseService
                .Get<SettingsViewModel>(id);

            return this.View(course);
        }

        [HttpPost]
        [Authorize(Policy = CourseRoleName.Admin)]
        public async Task<IActionResult> AddToAdmin(AddToAdminInputModel input) //Move to courseUsers
        {
            var user = await this.userManager.FindByIdAsync(input.UserId);
            await this.userManager.AddClaimAsync(user, new Claim(CourseRoleName.Admin, input.CourseId));

            return this.RedirectToAction(nameof(this.Settings), new { id = input.CourseId });
        }
    }
}
