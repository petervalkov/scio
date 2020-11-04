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
    using Scio.Web.ViewModels;
    using Scio.Web.ViewModels.Forum.Common;
    using Scio.Web.ViewModels.Forum.Posts;

    [Area("Forum")]
    public class PostsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IForumPostService forumPostService;

        public PostsController(
            UserManager<ApplicationUser> userManager,
            IForumPostService forumPostService)
        {
            this.userManager = userManager;
            this.forumPostService = forumPostService;
        }

        [HttpGet]
        [Route("Forum")]
        [Route("Forum/Posts")]
        [AllowAnonymous]
        public IActionResult All()
        {
            var viewModel = this.forumPostService
                .Get<QuestionViewModel>();

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

            var viewModel = this.forumPostService
                .Get<DetailsViewModel>(id);

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult New() => this.View();

        [HttpPost]
        public async Task<IActionResult> New(CreateInputModel input)
        {
            if (this.ModelState.ContainsKey(ErrorMessage.InvalidRequest) ||
                (input.QuestionId != null && this.forumPostService.Get<ValidationModel>(input.QuestionId) == null))
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
                    .Get<DetailsViewModel>(input.QuestionId);

                return this.View(nameof(this.Details), question);
            }

            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
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
        public IActionResult Edit([Required] string id)
        {
            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
            var post = this
                .forumPostService.Get<EditViewModel>(id, userId);

            if (post == null)
            {
                return this.BadRequest();
            }

            return this.View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditInputModel input)
        {
            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            if (this.ModelState.ContainsKey(ErrorMessage.InvalidRequest)
                || this.forumPostService.Find<ValidationModel>(input.Id, userId, input.QuestionId) == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                var errorModel = this.forumPostService.Get<EditViewModel>(input.Id);
                errorModel.Title = errorModel.QuestionId == null ? input.Title : errorModel.Title;
                errorModel.Body = errorModel.QuestionId == null ? input.Body : errorModel.Body;
                errorModel.AnswerBody = input.AnswerBody;

                return this.View(errorModel);
            }

            await this.forumPostService
                .EditAsync(input.Id, input.Title, input.AnswerBody ?? input.Body);

            return this.RedirectToAction(nameof(this.Details), new { id = input.QuestionId ?? input.Id });
        }

        [HttpGet]
        [ValidateModelState]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var userId = this.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
            var post = this.forumPostService
                .Find<ValidationModel>(id, userId);

            if (post == null)
            {
                return this.BadRequest();
            }

            await this.forumPostService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
