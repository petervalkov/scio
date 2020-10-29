namespace Scio.Web.Areas.Forum.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Scio.Common;
    using Scio.Data.Models;
    using Scio.Services.Data;
    using Scio.Web.Infrastructure.Validation;
    using Scio.Web.ViewModels.Forum.Common;
    using Scio.Web.ViewModels.Forum.Posts;

    [Area("Forum")]
    public class PostsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAuthorizationService authorizationService;
        private readonly IForumPostService forumPostService;

        public PostsController(
            UserManager<ApplicationUser> userManager,
            IAuthorizationService authorizationService,
            IForumPostService forumPostService)
        {
            this.userManager = userManager;
            this.authorizationService = authorizationService;
            this.forumPostService = forumPostService;
        }

        [HttpGet]
        [Route("Forum")]
        [Route("Forum/Posts")]
        [AllowAnonymous]
        public IActionResult All()
        {
            var viewModel = this.forumPostService.GetAllQuestions<QuestionViewModel>();
            return this.View(viewModel);
        }

        [HttpGet]
        [Route("Forum/{id}")]
        [Route("Forum/Posts/{id}")]
        [AllowAnonymous]
        public IActionResult Details([Required] string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            var viewModel = this.forumPostService.GetById<DetailsViewModel>(id);
            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult New() => this.View();

        [HttpPost]
        public async Task<IActionResult> New(CreateInputModel input)
        {
            if (this.ModelState.ContainsKey(ErrorMessage.InvalidRequest) ||
                !(input.QuestionId == null || this.forumPostService.PostExist(input.QuestionId)))
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                if (input.QuestionId == null)
                {
                    return this.View(input);
                }

                var question = this.forumPostService
                    .GetById<DetailsViewModel>(input.QuestionId);

                return this.View(nameof(this.Details), question);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var postId = await this.forumPostService
                .CreateAsync(input.Title, input.Body ?? input.AnswerBody, input.QuestionId, userId);

            if (postId == null)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction(nameof(this.Details), new { id = input.QuestionId ?? postId });
        }

        [HttpGet]
        [ValidateModelState]
        public async Task<IActionResult> Edit([Required] string id)
        {
            var post = this.forumPostService.GetById<EditViewModel>(id);

            if (!(await this.authorizationService.AuthorizeAsync(this.User, post, "Resource")).Succeeded)
            {
                return this.BadRequest();
            }

            return this.View(post);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Edit(EditInputModel input)
        {
            var post = this.forumPostService.GetById<EditViewModel>(input.Id);

            if (post.QuestionId == null && input.Title == null)
            {
                return this.BadRequest();
            }

            if (!(await this.authorizationService.AuthorizeAsync(this.User, post, "Resource")).Succeeded)
            {
                return this.BadRequest();
            }

            await this.forumPostService.EditAsync(input.Id, input.Title, input.Body);

            var postId = post.QuestionId ?? input.Id;

            return this.RedirectToAction(nameof(this.Details), new { id = postId });
        }

        [HttpGet]
        [ValidateModelState]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var question = this.forumPostService.GetById<ResourceOwnerValidationModel>(id);

            if (!(await this.authorizationService.AuthorizeAsync(this.User, question, "Resource")).Succeeded)
            {
                return this.BadRequest();
            }

            await this.forumPostService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
