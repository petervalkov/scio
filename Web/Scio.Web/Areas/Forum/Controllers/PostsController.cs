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
        [AllowAnonymous]
        public IActionResult All()
        {
            var viewModel = this.forumPostService.GetAllQuestions<QuestionViewModel>();
            return this.View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Details([Required] string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            var viewModel = this.forumPostService.GetById<QuestionDetailsViewModel>(id);
            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult AskQuestion(CreateQuestionViewModel viewModel)
        {
            return this.View(viewModel);
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> AskQuestion(CreateQuestionInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var questionId = await this.forumPostService.CreateQuestionAsync(input.Title, input.Body, user.Id);

            if (questionId != null)
            {
                return this.RedirectToAction(nameof(this.Details), new { id = questionId });
            }

            return this.View(input);
        }

        [HttpGet]
        public async Task<IActionResult> EditQuestion([Required] string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            var question = this.forumPostService.GetById<EditQuestionViewModel>(id);

            var isAuthorized = await this.authorizationService.AuthorizeAsync(this.User, question, "Resource");
            if (!isAuthorized.Succeeded)
            {
                return this.Forbid();
            }

            return this.View(question);
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> EditQuestion(EditQuestionInputModel input)
        {
            var question = this.forumPostService.GetById<EditQuestionViewModel>(input.Id);

            var isAuthorized = await this.authorizationService.AuthorizeAsync(this.User, question, "Resource");
            if (!isAuthorized.Succeeded)
            {
                return this.Forbid();
            }

            await this.forumPostService.EditQuestionAsync(input.Id, input.Title, input.Body);
            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        public async Task<IActionResult> AnswerQuestion(CreateAnswerInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                await this.forumPostService.CreateAnswerAsync(input.Body, input.QuestionId, user.Id);
            }

            return this.RedirectToAction(nameof(this.Details), new { id = input.QuestionId });
        }

        [HttpGet]
        public async Task<IActionResult> EditAnswer([Required] string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            var answer = this.forumPostService.GetById<EditAnswerViewModel>(id);

            var isAuthorized = await this.authorizationService.AuthorizeAsync(this.User, answer, "Resource");
            if (!isAuthorized.Succeeded)
            {
                return this.Forbid();
            }

            return this.View(answer);
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> EditAnswer(EditAnswerInputModel input)
        {
            var answer = this.forumPostService.GetById<EditAnswerInputModel>(input.Id);

            var isAuthorized = await this.authorizationService.AuthorizeAsync(this.User, answer, "Resource");
            if (!isAuthorized.Succeeded)
            {
                return this.Forbid();
            }

            await this.forumPostService.EditAnswerAsync(input.Id, input.Body);
            return this.RedirectToAction(nameof(this.Details), new { id = answer.QuestionId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete([Required] string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            var question = this.forumPostService.GetById<ResourceOwnerValidationModel>(id);

            var isAuthorized = await this.authorizationService.AuthorizeAsync(this.User, question, "Resource");
            if (!isAuthorized.Succeeded)
            {
                return this.Forbid();
            }

            await this.forumPostService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
