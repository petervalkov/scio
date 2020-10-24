namespace Scio.Web.Areas.Forum.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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
        public IActionResult New()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> New(CreateInputModel input)
        {
            bool isQuestion = input.QuestionId == null;

            if (!this.ModelState.IsValid)
            {
                if (isQuestion)
                {
                    return this.View(input);
                }

                var questionId = this.forumPostService.GetById<DetailsViewModel>(input.QuestionId);
                if (questionId == null)
                {
                    return this.BadRequest();
                }

                return this.View(nameof(this.Details), questionId);
            }

            var userId = this.userManager.GetUserId(this.User);

            if (isQuestion)
            {
                string questionId = await this.forumPostService.CreateAsync(input.Title, input.Body, null, userId);
                if (questionId != null)
                {
                    return this.RedirectToAction(nameof(this.Details), new { id = questionId });
                }
            }
            else
            {
                if (this.forumPostService.PostExist(input.QuestionId))
                {
                    string answerId = await this.forumPostService.CreateAsync(null, input.AnswerBody, input.QuestionId, userId);
                    if (answerId != null)
                    {
                        return this.RedirectToAction(nameof(this.Details), new { id = input.QuestionId });
                    }
                }
            }

            return this.BadRequest();
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
